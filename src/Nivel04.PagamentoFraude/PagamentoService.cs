namespace Nivel04.PagamentoFraude;

// NIVEL 4 - SO AS REGRAS DE NEGOCIO. VOCE ESCREVE OS TESTES COM MOQ E O CÓDIGO.
//
// Leia o arquivo REGRAS-DE-NEGOCIO.md para entender o que deve ser implementado.
//
// As interfaces IGatewayPagamento e IDetectorFraude já foram fornecidas.
// Nos testes, você vai mockar essas dependências com Moq.
//
// Para rodar: dotnet test tests/Nivel04.PagamentoFraude.Tests

public class PagamentoService
{
    private readonly IGatewayPagamento _gateway;
    private readonly IDetectorFraude _detectorFraude;

    public PagamentoService(IGatewayPagamento gateway, IDetectorFraude detectorFraude)
    {
        _gateway = gateway;
        _detectorFraude = detectorFraude;
    }

    // TODO: implemente os métodos de pagamento seguindo as regras de negócio
    // Dica: comece escrevendo os testes com Mock e deixe a implementação guiar você
}
