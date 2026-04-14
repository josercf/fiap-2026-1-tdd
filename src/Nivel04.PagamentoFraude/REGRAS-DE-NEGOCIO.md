# Nivel 4 – Processamento de Pagamento com Detecção de Fraude

## Contexto

Você deve implementar um `PagamentoService` que processa pagamentos de cartão de crédito.
Este serviço depende de duas interfaces externas que você irá **mockar** com Moq.

## Dependências (já fornecidas)

```csharp
// Processa o pagamento no gateway externo
public interface IGatewayPagamento
{
    ResultadoPagamento Processar(string cartao, decimal valor);
}

// Detecta se uma transação é suspeita de fraude
public interface IDetectorFraude
{
    bool EhSuspeito(string cartao, decimal valor);
}

public record ResultadoPagamento(bool Sucesso, string? MensagemErro = null);
```

## Regras de Negócio

### Processamento Normal

1. Dado que o pagamento **não é suspeito** e o **gateway retorna sucesso**,
   o método `Pagar(string cartao, decimal valor)` deve retornar `ResultadoPagamento(Sucesso: true)`

2. O detector de fraude deve ser **sempre consultado** antes do gateway

### Bloqueio por Fraude

3. Se o detector de fraude indicar que a transação é suspeita (`EhSuspeito = true`),
   o pagamento deve ser **bloqueado sem acionar o gateway**
   e retornar `ResultadoPagamento(Sucesso: false, MensagemErro: "Transação bloqueada por suspeita de fraude.")`

4. Quando bloqueado por fraude, o método `Processar` do gateway **NÃO deve ser chamado**

### Falha no Gateway

5. Se o gateway retornar `Sucesso: false`, o `PagamentoService` deve repassar o resultado sem alteração

### Validações

6. Valor de pagamento **zero ou negativo** lança `ArgumentException`
7. Cartão **nulo ou vazio** lança `ArgumentException`

## Como usar Moq nos testes

```csharp
// 1. Crie o mock da dependência
var mockGateway = new Mock<IGatewayPagamento>();
var mockFraude = new Mock<IDetectorFraude>();

// 2. Configure o comportamento (stub)
mockGateway.Setup(g => g.Processar(It.IsAny<string>(), It.IsAny<decimal>()))
           .Returns(new ResultadoPagamento(true));
mockFraude.Setup(f => f.EhSuspeito(It.IsAny<string>(), It.IsAny<decimal>()))
          .Returns(false);

// 3. Injete no serviço
var service = new PagamentoService(mockGateway.Object, mockFraude.Object);

// 4. Execute e verifique
var resultado = service.Pagar("1234-5678-9012-3456", 100m);
resultado.Sucesso.Should().BeTrue();

// 5. Verifique interações (mock verify)
mockGateway.Verify(g => g.Processar("1234-5678-9012-3456", 100m), Times.Once);
```
