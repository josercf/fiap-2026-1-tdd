using FluentAssertions;
using Nivel03.CarrinhoCompras;
using Xunit;

namespace Nivel03.CarrinhoCompras.Tests;

// GABARITO - Nivel 3: Carrinho de Compras

public class CarrinhoComprasTests
{
    private static Produto CriaProduto(string id = "p1", string nome = "Produto", decimal preco = 10m)
        => new(id, nome, preco);

    // --- ESTADO INICIAL ---

    [Fact]
    public void CarrinhoVazio_Total_DeveSerZero()
    {
        var carrinho = new CarrinhoCompras();
        carrinho.CalcularTotal().Should().Be(0);
    }

    [Fact]
    public void CarrinhoVazio_Itens_DeveEstarVazio()
    {
        var carrinho = new CarrinhoCompras();
        carrinho.Itens.Should().BeEmpty();
    }

    // --- ADIÇÃO DE ITENS ---

    [Fact]
    public void Adicionar_UmProduto_CarrinhoTemUmItem()
    {
        var carrinho = new CarrinhoCompras();
        carrinho.Adicionar(CriaProduto(), 1);
        carrinho.Itens.Should().HaveCount(1);
    }

    [Fact]
    public void Adicionar_MesmoProdutoDuasVezes_SomaQuantidades()
    {
        var carrinho = new CarrinhoCompras();
        var produto = CriaProduto();

        carrinho.Adicionar(produto, 2);
        carrinho.Adicionar(produto, 3);

        carrinho.Itens.Should().HaveCount(1);
        carrinho.Itens[0].Quantidade.Should().Be(5);
    }

    [Fact]
    public void Adicionar_QuantidadeZero_LancaExcecao()
    {
        var carrinho = new CarrinhoCompras();
        Action acao = () => carrinho.Adicionar(CriaProduto(), 0);
        acao.Should().Throw<ProdutoInvalidoException>();
    }

    [Fact]
    public void Adicionar_QuantidadeNegativa_LancaExcecao()
    {
        var carrinho = new CarrinhoCompras();
        Action acao = () => carrinho.Adicionar(CriaProduto(), -1);
        acao.Should().Throw<ProdutoInvalidoException>();
    }

    [Fact]
    public void Adicionar_ProdutoComPrecoNegativo_LancaExcecao()
    {
        var carrinho = new CarrinhoCompras();
        Action acao = () => carrinho.Adicionar(CriaProduto(preco: -5m), 1);
        acao.Should().Throw<ProdutoInvalidoException>();
    }

    // --- REMOÇÃO DE ITENS ---

    [Fact]
    public void Remover_ProdutoExistente_RemoveDoCarrinho()
    {
        var carrinho = new CarrinhoCompras();
        carrinho.Adicionar(CriaProduto("p1"), 1);
        carrinho.Adicionar(CriaProduto("p2"), 1);

        carrinho.Remover("p1");

        carrinho.Itens.Should().HaveCount(1);
        carrinho.Itens[0].Produto.Id.Should().Be("p2");
    }

    [Fact]
    public void Remover_ProdutoInexistente_NaoLancaExcecao()
    {
        var carrinho = new CarrinhoCompras();
        Action acao = () => carrinho.Remover("inexistente");
        acao.Should().NotThrow();
    }

    // --- CÁLCULO DE TOTAL ---

    [Fact]
    public void CalcularTotal_DoisProdutos_RetornaSomaCorreta()
    {
        var carrinho = new CarrinhoCompras();
        carrinho.Adicionar(new Produto("p1", "Camiseta", 50m), 2);
        carrinho.Adicionar(new Produto("p2", "Calça", 100m), 1);

        carrinho.CalcularTotal().Should().Be(200m);
    }

    // --- DESCONTO ---

    [Fact]
    public void CalcularTotalComDesconto_10Porcento_RetornaValorCorreto()
    {
        var carrinho = new CarrinhoCompras();
        carrinho.Adicionar(new Produto("p1", "Produto", 100m), 1);

        carrinho.CalcularTotalComDesconto(10).Should().Be(90m);
    }

    [Fact]
    public void CalcularTotalComDesconto_MaiorQue100_LancaExcecao()
    {
        var carrinho = new CarrinhoCompras();
        Action acao = () => carrinho.CalcularTotalComDesconto(101);
        acao.Should().Throw<DescontoInvalidoException>();
    }

    [Fact]
    public void CalcularTotalComDesconto_Negativo_LancaExcecao()
    {
        var carrinho = new CarrinhoCompras();
        Action acao = () => carrinho.CalcularTotalComDesconto(-1);
        acao.Should().Throw<DescontoInvalidoException>();
    }

    // --- CUPOM ---

    [Fact]
    public void AplicarCupom_FIAP10_Aplica10PorcentoDeDesconto()
    {
        var carrinho = new CarrinhoCompras();
        carrinho.Adicionar(new Produto("p1", "Produto", 200m), 1);

        var total = carrinho.AplicarCupom("FIAP10");

        total.Should().Be(180m);
    }

    [Fact]
    public void AplicarCupom_FIAP20_Aplica20PorcentoDeDesconto()
    {
        var carrinho = new CarrinhoCompras();
        carrinho.Adicionar(new Produto("p1", "Produto", 200m), 1);

        var total = carrinho.AplicarCupom("FIAP20");

        total.Should().Be(160m);
    }

    [Fact]
    public void AplicarCupom_CupomInvalido_LancaExcecao()
    {
        var carrinho = new CarrinhoCompras();
        Action acao = () => carrinho.AplicarCupom("INVALIDO");
        acao.Should().Throw<CupomInvalidoException>();
    }

    [Fact]
    public void AplicarCupom_DuasVezes_LancaExcecao()
    {
        var carrinho = new CarrinhoCompras();
        carrinho.Adicionar(new Produto("p1", "Produto", 100m), 1);
        carrinho.AplicarCupom("FIAP10");

        Action acao = () => carrinho.AplicarCupom("FIAP10");
        acao.Should().Throw<CupomInvalidoException>();
    }
}
