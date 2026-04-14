using FluentAssertions;
using Nivel02.ValidacaoCpf;
using Xunit;

namespace Nivel02.ValidacaoCpf.Tests;

// NIVEL 2 - TESTES FORNECIDOS
// Os testes abaixo descrevem o comportamento esperado do CpfValidator.
// Você NÃO deve alterar este arquivo.
// Implemente o código em src/Nivel02.ValidacaoCpf/CpfValidator.cs

public class CpfValidatorTests
{
    private readonly CpfValidator _validator = new();

    // --- CASOS INVÁLIDOS ---

    [Fact]
    public void IsValid_CpfNulo_RetornaFalso()
    {
        _validator.IsValid(null!).Should().BeFalse();
    }

    [Fact]
    public void IsValid_CpfVazio_RetornaFalso()
    {
        _validator.IsValid("").Should().BeFalse();
    }

    [Fact]
    public void IsValid_CpfSoEspacos_RetornaFalso()
    {
        _validator.IsValid("   ").Should().BeFalse();
    }

    [Fact]
    public void IsValid_CpfComLetras_RetornaFalso()
    {
        _validator.IsValid("abc.def.ghi-jk").Should().BeFalse();
    }

    [Theory]
    [InlineData("123.456.789")]          // faltam 2 dígitos
    [InlineData("123.456.789-0011")]     // dígitos a mais
    [InlineData("1234")]                 // muito curto
    public void IsValid_CpfComTamanhoErrado_RetornaFalso(string cpf)
    {
        _validator.IsValid(cpf).Should().BeFalse();
    }

    [Theory]
    [InlineData("000.000.000-00")]
    [InlineData("111.111.111-11")]
    [InlineData("222.222.222-22")]
    [InlineData("999.999.999-99")]
    public void IsValid_CpfComTodosDigitosIguais_RetornaFalso(string cpf)
    {
        _validator.IsValid(cpf).Should().BeFalse();
    }

    [Theory]
    [InlineData("111.222.333-44")]
    [InlineData("123.456.789-00")]
    [InlineData("987.654.321-11")]
    public void IsValid_CpfComDigitoVerificadorErrado_RetornaFalso(string cpf)
    {
        _validator.IsValid(cpf).Should().BeFalse();
    }

    // --- CASOS VÁLIDOS ---

    [Theory]
    [InlineData("529.982.247-25")]   // formato com pontuação
    [InlineData("52998224725")]      // formato sem pontuação (mesmo CPF anterior)
    [InlineData("123.456.789-09")]
    [InlineData("111.444.777-35")]
    public void IsValid_CpfValido_RetornaVerdadeiro(string cpf)
    {
        _validator.IsValid(cpf).Should().BeTrue();
    }
}
