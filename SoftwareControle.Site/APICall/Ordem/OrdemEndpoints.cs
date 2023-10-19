using Newtonsoft.Json;
using SoftwareControle.Models;

namespace SoftwareControle.Site.APICall.Ordem;

public class OrdemEndpoints : IOrdemEndpoints
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger<OrdemEndpoints> _logger;

    public OrdemEndpoints(HttpClient client,
    IConfiguration config,
    ILogger<OrdemEndpoints> logger)
    {
        _client = client;
        _config = config;
        _logger = logger;
    }

    public async Task<List<OrdemModel>?> Buscar()
    {
        string buscarTodosEndpoint = _config["apiLocation"] + _config["buscarOrdem"];
        var authResult = await _client.GetAsync(buscarTodosEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro durante o carregamento das ordens: {authContent}",
                authContent);

            return null;
        }

        var ordemModel = JsonConvert.DeserializeObject<List<OrdemModel>>(authContent);

        return ordemModel;
    }

    public async Task<OrdemModel?> BuscarPorId(Guid id)
    {
        string buscarPorIdEndpoint = _config["apiLocation"] + _config["buscarOrdemPorId"] + $"/{id}";
        var authResult = await _client.GetAsync(buscarPorIdEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro durante o carregamento da ordem por Id: {authContent}",
                authContent);

            return null;
        }

        var ordemModel = JsonConvert.DeserializeObject<OrdemModel>(authContent);

        return ordemModel;
    }
    public async Task<string?> Criar(OrdemModel ordem)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Descricao", ordem.Descricao),
            new KeyValuePair<string, string>("DataPrazoMaximo", ordem.DataPrazoMaximo.ToString()),
            new KeyValuePair<string, string>("NivelUrgencia", ordem.NivelUrgencia.ToString()),
            new KeyValuePair<string, string>("Situacao", ordem.Situacao.ToString()),
            new KeyValuePair<string, string>("UsuarioId", ordem.UsuarioId.ToString()),
            new KeyValuePair<string, string>("FerramentaId", ordem.FerramentaId.ToString()),
            new KeyValuePair<string, string>("NomeFerramenta", ordem.NomeFerramenta.ToString()),
        });

        string criarUsuarioEndpoint = _config["apiLocation"] + _config["criarOrdem"];
        var authResult = await _client.PostAsync(criarUsuarioEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu para registrar a ferramenta: {authContent}",
                authContent);

            return await authResult.Content.ReadAsStringAsync();
        }

        return null;
    }

    public async Task<string?> Atualizar(OrdemModel ordem)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Id", ordem.Id.ToString()),
            new KeyValuePair<string, string>("Descricao", ordem.Descricao),
            new KeyValuePair<string, string>("DataPrazoMaximo", ordem.DataPrazoMaximo.ToString()),
            new KeyValuePair<string, string>("NivelUrgencia", ordem.NivelUrgencia.ToString()),
            new KeyValuePair<string, string>("Situacao", ordem.Situacao.ToString()),
            new KeyValuePair<string, string>("NomeResponsavel", ordem.NomeResponsavel!.ToString()),
            new KeyValuePair<string, string>("NomeFerramenta", ordem.NomeFerramenta.ToString()),
            new KeyValuePair<string, string>("DescricaoResponsavel", ordem.DescricaoResponsavel?.ToString() ?? ""),
			new KeyValuePair<string, string>("DataIniciado", ordem.DataIniciado?.ToString() ?? ""),
			new KeyValuePair<string, string>("DataFinalizado", ordem.DataFinalizado?.ToString() ?? ""),
			new KeyValuePair<string, string>("HorasTrabalhadas", ordem.HorasTrabalhadas?.ToString() ?? ""),
		});

        string atualizarEndpoint = _config["apiLocation"] + _config["atualizarOrdem"];
        var authResult = await _client.PutAsync(atualizarEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro ao atualizar a ordem: {authContent}",
                authContent);

            return await authResult.Content.ReadAsStringAsync(); ;
        }

        return null;
    }
    public async Task<string?> Deletar(Guid id)
    {
        string deletarEndpoint = _config["apiLocation"] + _config["deletarOrdem"] + $"/{id}";
        var authResult = await _client.DeleteAsync(deletarEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogError("Ocorreu um erro para deletar a ordem: {authContent}",
                authContent);

            return await authResult.Content.ReadAsStringAsync(); ;
        }

        return null;
    }

        public async Task<List<OrdemModel>?> BuscarPorNomeResponsavel(string nomeResponsavel)
    {
        string buscarPorIdEndpoint = _config["apiLocation"] 
            + _config["buscarOrdemPorNomeResponsavel"] 
            + $"/{nomeResponsavel}";
        var authResult = await _client.GetAsync(buscarPorIdEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro durante o carregamento da ordem por nome do responsavel: {authContent}",
                authContent);

            return null;
        }

        var ordemModel = JsonConvert.DeserializeObject<List<OrdemModel>>(authContent);

        return ordemModel;
    }
}