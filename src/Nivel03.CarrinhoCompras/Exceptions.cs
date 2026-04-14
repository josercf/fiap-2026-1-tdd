namespace Nivel03.CarrinhoCompras;

public class ProdutoInvalidoException : Exception
{
    public ProdutoInvalidoException(string mensagem) : base(mensagem) { }
}

public class CupomInvalidoException : Exception
{
    public CupomInvalidoException(string cupom) : base($"Cupom '{cupom}' não é válido.") { }
}

public class DescontoInvalidoException : Exception
{
    public DescontoInvalidoException(string mensagem) : base(mensagem) { }
}
