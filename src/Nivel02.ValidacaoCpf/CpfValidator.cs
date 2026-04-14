namespace Nivel02.ValidacaoCpf;

// NIVEL 2 - TESTES FORNECIDOS, VOCE IMPLEMENTA O CÓDIGO
//
// Os testes já estão escritos em tests/Nivel02.ValidacaoCpf.Tests/CpfValidatorTests.cs
// Sua missão: implementar este método para que TODOS os testes passem.
//
// Regras do algoritmo CPF:
//   1. Remove caracteres não numéricos (pontos e traço)
//   2. Deve ter exatamente 11 dígitos
//   3. Não pode ter todos os dígitos iguais (ex: "111.111.111-11")
//   4. Calcula e valida o 1º dígito verificador
//   5. Calcula e valida o 2º dígito verificador
//
// Dica para o cálculo dos dígitos verificadores:
//   Primeiro dígito:  soma = Σ (digito[i] * (10 - i)) para i de 0 a 8
//                     resto = soma % 11
//                     dígito = (resto < 2) ? 0 : 11 - resto
//   Segundo dígito:   soma = Σ (digito[i] * (11 - i)) para i de 0 a 9
//                     (mesma lógica do resto)
//
// Para rodar: dotnet test tests/Nivel02.ValidacaoCpf.Tests

public class CpfValidator
{
    public bool IsValid(string cpf)
    {
        // TODO: implemente a validação de CPF aqui
        throw new NotImplementedException("Implemente a validação de CPF!");
    }
}
