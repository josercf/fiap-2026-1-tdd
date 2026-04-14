namespace Nivel02.ValidacaoCpf;

// GABARITO - Nivel 2: Validação de CPF

public class CpfValidator
{
    public bool IsValid(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;

        // Remove pontuação
        var digits = cpf.Replace(".", "").Replace("-", "").Trim();

        // Deve ter exatamente 11 dígitos numéricos
        if (digits.Length != 11 || !digits.All(char.IsDigit))
            return false;

        // Não pode ter todos os dígitos iguais
        if (digits.Distinct().Count() == 1)
            return false;

        // Valida o 1º dígito verificador
        var d1 = CalcularDigito(digits, 10);
        if (d1 != int.Parse(digits[9].ToString()))
            return false;

        // Valida o 2º dígito verificador
        var d2 = CalcularDigito(digits, 11);
        if (d2 != int.Parse(digits[10].ToString()))
            return false;

        return true;
    }

    private static int CalcularDigito(string digits, int pesoInicial)
    {
        var soma = 0;
        for (int i = 0; i < pesoInicial - 1; i++)
            soma += int.Parse(digits[i].ToString()) * (pesoInicial - i);

        var resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}
