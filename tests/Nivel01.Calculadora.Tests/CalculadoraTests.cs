using FluentAssertions;
using Nivel01.Calculadora;
using Xunit;

namespace Nivel01.Calculadora.Tests;

// NIVEL 1 - EXEMPLO COMPLETO
// Todos os testes e a implementação estão prontos.
// Objetivo: entender o padrão AAA (Arrange, Act, Assert) e o uso de xUnit + FluentAssertions.
//
// Para rodar: dotnet test tests/Nivel01.Calculadora.Tests

public class CalculadoraTests
{
    private readonly Calculadora _calculadora = new();

    // --- SOMA ---

    [Fact]
    public void Soma_DoisNumerosPositivos_RetornaSomaCorreta()
    {
        // Arrange
        int a = 3, b = 7;

        // Act
        int resultado = _calculadora.Soma(a, b);

        // Assert
        resultado.Should().Be(10);
    }

    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(0, 0, 0)]
    [InlineData(-1, 1, 0)]
    [InlineData(-5, -3, -8)]
    [InlineData(100, -100, 0)]
    public void Soma_VariosValores_RetornaSomaCorreta(int a, int b, int esperado)
    {
        // Arrange, Act e Assert em uma linha (aceitável em testes parametrizados simples)
        _calculadora.Soma(a, b).Should().Be(esperado);
    }

    // --- SUBTRAÇÃO ---

    [Fact]
    public void Subtracao_NumerosPositivos_RetornaResultadoCorreto()
    {
        // Arrange
        int a = 10, b = 3;

        // Act
        int resultado = _calculadora.Subtracao(a, b);

        // Assert
        resultado.Should().Be(7);
    }

    [Fact]
    public void Subtracao_ResultadoNegativo_RetornaValorNegativo()
    {
        _calculadora.Subtracao(3, 10).Should().Be(-7);
    }

    // --- MULTIPLICAÇÃO ---

    [Fact]
    public void Multiplicacao_NumerosPositivos_RetornaResultadoCorreto()
    {
        _calculadora.Multiplicacao(4, 5).Should().Be(20);
    }

    [Fact]
    public void Multiplicacao_PorZero_RetornaZero()
    {
        _calculadora.Multiplicacao(999, 0).Should().Be(0);
    }

    // --- DIVISÃO ---

    [Fact]
    public void Divisao_NumerosValidos_RetornaQuocienteCorreto()
    {
        // Arrange
        double dividendo = 10, divisor = 4;

        // Act
        double resultado = _calculadora.Divisao(dividendo, divisor);

        // Assert
        resultado.Should().BeApproximately(2.5, precision: 0.001);
    }

    [Fact]
    public void Divisao_PorZero_LancaDivideByZeroException()
    {
        // Arrange
        Action acao = () => _calculadora.Divisao(10, 0);

        // Assert
        acao.Should()
            .Throw<DivideByZeroException>()
            .WithMessage("Divisão por zero não é permitida.");
    }
}
