using FluentAssertions;
using Moq;
using Nivel04.PagamentoFraude;
using Xunit;

namespace Nivel04.PagamentoFraude.Tests;

// GABARITO - Nivel 4: Processamento de Pagamento com Detecção de Fraude

public class PagamentoServiceTests
{
    private const string CartaoValido = "4111-1111-1111-1111";
    private const decimal ValorValido = 250m;

    private readonly Mock<IGatewayPagamento> _mockGateway = new();
    private readonly Mock<IDetectorFraude> _mockFraude = new();
    private readonly PagamentoService _service;

    public PagamentoServiceTests()
    {
        _service = new PagamentoService(_mockGateway.Object, _mockFraude.Object);
    }

    // --- PAGAMENTO COM SUCESSO ---

    [Fact]
    public void Pagar_TransacaoLimpa_RetornaSucesso()
    {
        // Arrange
        _mockFraude.Setup(f => f.EhSuspeito(CartaoValido, ValorValido)).Returns(false);
        _mockGateway.Setup(g => g.Processar(CartaoValido, ValorValido))
                    .Returns(new ResultadoPagamento(true));

        // Act
        var resultado = _service.Pagar(CartaoValido, ValorValido);

        // Assert
        resultado.Sucesso.Should().BeTrue();
    }

    [Fact]
    public void Pagar_TransacaoLimpa_DetectorFraudeEGatewaySaoConsultados()
    {
        // Arrange
        _mockFraude.Setup(f => f.EhSuspeito(It.IsAny<string>(), It.IsAny<decimal>())).Returns(false);
        _mockGateway.Setup(g => g.Processar(It.IsAny<string>(), It.IsAny<decimal>()))
                    .Returns(new ResultadoPagamento(true));

        // Act
        _service.Pagar(CartaoValido, ValorValido);

        // Assert - verifica que ambas as dependências foram chamadas
        _mockFraude.Verify(f => f.EhSuspeito(CartaoValido, ValorValido), Times.Once);
        _mockGateway.Verify(g => g.Processar(CartaoValido, ValorValido), Times.Once);
    }

    // --- BLOQUEIO POR FRAUDE ---

    [Fact]
    public void Pagar_TransacaoSuspeita_RetornaFalhaComMensagemDeFraude()
    {
        // Arrange
        _mockFraude.Setup(f => f.EhSuspeito(CartaoValido, ValorValido)).Returns(true);

        // Act
        var resultado = _service.Pagar(CartaoValido, ValorValido);

        // Assert
        resultado.Sucesso.Should().BeFalse();
        resultado.MensagemErro.Should().Contain("fraude");
    }

    [Fact]
    public void Pagar_TransacaoSuspeita_GatewayNaoEConsultado()
    {
        // Arrange
        _mockFraude.Setup(f => f.EhSuspeito(It.IsAny<string>(), It.IsAny<decimal>())).Returns(true);

        // Act
        _service.Pagar(CartaoValido, ValorValido);

        // Assert - o gateway NUNCA deve ser chamado quando detectado fraude
        _mockGateway.Verify(g => g.Processar(It.IsAny<string>(), It.IsAny<decimal>()), Times.Never);
    }

    // --- FALHA NO GATEWAY ---

    [Fact]
    public void Pagar_GatewayRetornaFalha_RepassaResultado()
    {
        // Arrange
        _mockFraude.Setup(f => f.EhSuspeito(It.IsAny<string>(), It.IsAny<decimal>())).Returns(false);
        _mockGateway.Setup(g => g.Processar(It.IsAny<string>(), It.IsAny<decimal>()))
                    .Returns(new ResultadoPagamento(false, "Cartão recusado."));

        // Act
        var resultado = _service.Pagar(CartaoValido, ValorValido);

        // Assert
        resultado.Sucesso.Should().BeFalse();
        resultado.MensagemErro.Should().Be("Cartão recusado.");
    }

    // --- VALIDAÇÕES DE ENTRADA ---

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Pagar_CartaoNuloOuVazio_LancaArgumentException(string? cartao)
    {
        Action acao = () => _service.Pagar(cartao!, ValorValido);
        acao.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-100)]
    public void Pagar_ValorZeroOuNegativo_LancaArgumentException(decimal valor)
    {
        Action acao = () => _service.Pagar(CartaoValido, valor);
        acao.Should().Throw<ArgumentException>();
    }
}
