namespace Nivel04.PagamentoFraude;

// GABARITO - Nivel 4: Processamento de Pagamento com Detecção de Fraude

public class PagamentoService
{
    private readonly IGatewayPagamento _gateway;
    private readonly IDetectorFraude _detectorFraude;

    public PagamentoService(IGatewayPagamento gateway, IDetectorFraude detectorFraude)
    {
        _gateway = gateway;
        _detectorFraude = detectorFraude;
    }

    public ResultadoPagamento Pagar(string cartao, decimal valor)
    {
        if (string.IsNullOrWhiteSpace(cartao))
            throw new ArgumentException("O número do cartão não pode ser vazio.", nameof(cartao));

        if (valor <= 0)
            throw new ArgumentException("O valor do pagamento deve ser maior que zero.", nameof(valor));

        // Sempre consulta o detector de fraude antes do gateway
        if (_detectorFraude.EhSuspeito(cartao, valor))
            return new ResultadoPagamento(false, "Transação bloqueada por suspeita de fraude.");

        return _gateway.Processar(cartao, valor);
    }
}
