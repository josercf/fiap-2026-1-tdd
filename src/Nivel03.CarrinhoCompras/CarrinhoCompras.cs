namespace Nivel03.CarrinhoCompras;

// GABARITO - Nivel 3: Carrinho de Compras

public class CarrinhoCompras
{
    private readonly Dictionary<string, (Produto Produto, int Quantidade)> _itens = new();
    private bool _cupomAplicado = false;

    private static readonly Dictionary<string, decimal> Cupons = new()
    {
        ["FIAP10"] = 10m,
        ["FIAP20"] = 20m,
    };

    public IReadOnlyList<(Produto Produto, int Quantidade)> Itens =>
        _itens.Values.ToList().AsReadOnly();

    public void Adicionar(Produto produto, int quantidade)
    {
        if (quantidade <= 0)
            throw new ProdutoInvalidoException("A quantidade deve ser maior que zero.");

        if (produto.Preco < 0)
            throw new ProdutoInvalidoException("O preço do produto não pode ser negativo.");

        if (_itens.TryGetValue(produto.Id, out var existente))
            _itens[produto.Id] = (produto, existente.Quantidade + quantidade);
        else
            _itens[produto.Id] = (produto, quantidade);
    }

    public void Remover(string produtoId)
    {
        _itens.Remove(produtoId);
    }

    public decimal CalcularTotal()
    {
        return _itens.Values.Sum(i => i.Produto.Preco * i.Quantidade);
    }

    public decimal CalcularTotalComDesconto(decimal percentualDesconto)
    {
        if (percentualDesconto < 0 || percentualDesconto > 100)
            throw new DescontoInvalidoException("O percentual de desconto deve ser entre 0 e 100.");

        return CalcularTotal() * (1 - percentualDesconto / 100);
    }

    public decimal AplicarCupom(string cupom)
    {
        if (_cupomAplicado)
            throw new CupomInvalidoException(cupom);

        if (!Cupons.TryGetValue(cupom, out var desconto))
            throw new CupomInvalidoException(cupom);

        _cupomAplicado = true;
        return CalcularTotalComDesconto(desconto);
    }
}
