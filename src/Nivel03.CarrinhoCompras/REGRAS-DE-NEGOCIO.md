# Nivel 3 – Carrinho de Compras

## Contexto

Você deve implementar um serviço de carrinho de compras para um e-commerce.
**Usando TDD**, escreva os testes **antes** de implementar o código.

## Fluxo esperado

1. Escreva um teste que falha (RED)
2. Implemente o mínimo para o teste passar (GREEN)
3. Melhore o código (REFACTOR)
4. Repita para a próxima regra

## Regras de Negócio

### Adição de Itens

1. O carrinho aceita produtos com quantidade mínima de **1**
2. Se o produto já existe no carrinho, a **quantidade deve ser somada**
3. Não é permitido adicionar produto com quantidade **zero ou negativa** → lança `ProdutoInvalidoException`
4. Não é permitido adicionar produto com preço **negativo** → lança `ProdutoInvalidoException`

### Remoção de Itens

5. É possível remover um item específico pelo `Id` do produto
6. Remover um item que não existe no carrinho **não gera erro** (operação idempotente)

### Cálculo de Total

7. O total é a soma de `(Preco × Quantidade)` de cada item
8. Um carrinho vazio tem total **zero**

### Desconto Percentual

9. É possível aplicar um desconto em percentual (0 a 100%)
10. Desconto **maior que 100%** lança `DescontoInvalidoException`
11. Desconto **negativo** lança `DescontoInvalidoException`

### Cupom de Desconto

12. O cupom `"FIAP10"` dá **10% de desconto** sobre o total
13. O cupom `"FIAP20"` dá **20% de desconto** sobre o total
14. Cupom **inválido** lança `CupomInvalidoException`
15. Um cupom **só pode ser aplicado uma vez** por carrinho

## Modelos disponíveis

```csharp
// Produto.cs (já criado)
public record Produto(string Id, string Nome, decimal Preco);

// Exceptions.cs (já criado)
public class ProdutoInvalidoException : Exception { ... }
public class CupomInvalidoException : Exception { ... }
public class DescontoInvalidoException : Exception { ... }
```

## Sugestão de estrutura

```csharp
public class CarrinhoCompras
{
    public IReadOnlyList<(Produto Produto, int Quantidade)> Itens { get; }
    public void Adicionar(Produto produto, int quantidade) { ... }
    public void Remover(string produtoId) { ... }
    public decimal CalcularTotal() { ... }
    public decimal CalcularTotalComDesconto(decimal percentualDesconto) { ... }
    public decimal AplicarCupom(string cupom) { ... }
}
```
