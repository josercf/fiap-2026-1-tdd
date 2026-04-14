namespace Nivel04.PagamentoFraude;

// As interfaces abaixo representam dependências externas (gateway de pagamento e detector de fraude).
// Em produção, seriam implementações reais. Nos testes, usaremos Mocks com Moq.

public interface IGatewayPagamento
{
    ResultadoPagamento Processar(string cartao, decimal valor);
}

public interface IDetectorFraude
{
    bool EhSuspeito(string cartao, decimal valor);
}

public record ResultadoPagamento(bool Sucesso, string? MensagemErro = null);
