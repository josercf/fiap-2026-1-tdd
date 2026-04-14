# FIAP 2026.1 – TDD na Prática com C#

Repositório de exercícios da **Aula 08** — Test-Driven Development.

## Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- IDE: [Rider](https://www.jetbrains.com/rider/) ou [VS Code](https://code.visualstudio.com/) com extensão C#

## Setup

```bash
git clone https://github.com/josercf/fiap-2026-1-tdd.git
cd fiap-2026-1-tdd
dotnet restore
dotnet build
```

## Rodando os testes

```bash
# Todos os testes
dotnet test

# Apenas um nível
dotnet test tests/Nivel01.Calculadora.Tests
dotnet test tests/Nivel02.ValidacaoCpf.Tests
dotnet test tests/Nivel03.CarrinhoCompras.Tests
dotnet test tests/Nivel04.PagamentoFraude.Tests
```

---

## Estrutura dos Exercícios

A dificuldade aumenta progressivamente. **Siga a ordem!**

```
src/
├── Nivel01.Calculadora/          ← código completo (estude o padrão)
├── Nivel02.ValidacaoCpf/         ← testes prontos, você implementa o código
├── Nivel03.CarrinhoCompras/      ← só regras de negócio, você escreve tudo
└── Nivel04.PagamentoFraude/      ← igual ao N3, mas com Mocks (Moq)

tests/
├── Nivel01.Calculadora.Tests/    ← testes completos (leia e entenda)
├── Nivel02.ValidacaoCpf.Tests/   ← testes prontos (não altere!)
├── Nivel03.CarrinhoCompras.Tests/← você escreve os testes aqui
└── Nivel04.PagamentoFraude.Tests/← você escreve os testes aqui
```

---

## Nível 1 – Calculadora (Exemplo Completo)

**Objetivo:** Estudar o padrão. Não precisa alterar nada.

Observe em `tests/Nivel01.Calculadora.Tests/CalculadoraTests.cs`:
- O padrão **AAA** (Arrange, Act, Assert)
- Como usar `[Fact]` e `[Theory]` com `[InlineData]`
- Como usar **FluentAssertions** (`.Should().Be()`, `.Should().Throw<>()`)

---

## Nível 2 – Validação de CPF (Testes Prontos)

**Objetivo:** Fazer todos os testes passarem implementando `CpfValidator.IsValid()`.

1. Leia `tests/Nivel02.ValidacaoCpf.Tests/CpfValidatorTests.cs` (não altere)
2. Leia as dicas em `src/Nivel02.ValidacaoCpf/CpfValidator.cs`
3. Implemente o método `IsValid(string cpf)`
4. Rode `dotnet test tests/Nivel02.ValidacaoCpf.Tests` até todos passarem

---

## Nível 3 – Carrinho de Compras (TDD Livre)

**Objetivo:** Praticar TDD completo — você escreve os testes E o código.

1. Leia `src/Nivel03.CarrinhoCompras/REGRAS-DE-NEGOCIO.md`
2. Escreva o primeiro teste em `tests/Nivel03.CarrinhoCompras.Tests/CarrinhoComprasTests.cs`
3. Rode o teste (deve falhar – **RED**)
4. Implemente o mínimo em `src/Nivel03.CarrinhoCompras/CarrinhoCompras.cs` para passar (**GREEN**)
5. Refatore se necessário (**REFACTOR**)
6. Repita para cada regra de negócio

---

## Nível 4 – Pagamento com Detecção de Fraude (TDD + Moq)

**Objetivo:** TDD com dependências externas mockadas usando **Moq**.

1. Leia `src/Nivel04.PagamentoFraude/REGRAS-DE-NEGOCIO.md` (inclui exemplos de Moq)
2. Escreva os testes em `tests/Nivel04.PagamentoFraude.Tests/PagamentoServiceTests.cs`
3. Implemente em `src/Nivel04.PagamentoFraude/PagamentoService.cs`
4. Verifique interações com `mock.Verify()`

---

## Gabarito

Os exercícios resolvidos estão na branch `gabarito`:

```bash
git checkout gabarito
```

> Tente resolver antes de consultar! O aprendizado está no processo.

---

## Stack

| Ferramenta | Versão | Uso |
|---|---|---|
| .NET | 8.0 | Plataforma |
| xUnit | 2.7 | Framework de testes |
| FluentAssertions | 6.12 | Assertions legíveis |
| Moq | 4.20 | Mocking de dependências |
