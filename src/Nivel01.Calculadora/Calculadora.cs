namespace Nivel01.Calculadora;

// NIVEL 1 - EXEMPLO COMPLETO
// Código e testes fornecidos para estudo do padrão TDD.
// Observe como os testes foram escritos ANTES do código (Red → Green → Refactor).

public class Calculadora
{
    public int Soma(int a, int b) => a + b;

    public int Subtracao(int a, int b) => a - b;

    public int Multiplicacao(int a, int b) => a * b;

    public double Divisao(double dividendo, double divisor)
    {
        if (divisor == 0)
            throw new DivideByZeroException("Divisão por zero não é permitida.");

        return dividendo / divisor;
    }
}
