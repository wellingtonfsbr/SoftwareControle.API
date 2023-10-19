using Newtonsoft.Json;
using SoftwareControle.Site.Models;

namespace SoftwareControle.Site.APICall.Usuario;

public class RelatorioEndpoints : IRelatorioEndpoints
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger<RelatorioEndpoints> _logger;

    public RelatorioEndpoints(HttpClient client,
    IConfiguration config,
    ILogger<RelatorioEndpoints> logger)
    {
        _client = client;
        _config = config;
        _logger = logger;
    }
    public async Task<List<RelatorioModel>?> Buscar()
    {
        string buscarTodosEndpoint = _config["apiLocation"] + _config["buscarRelatorios"];
        var authResult = await _client.GetAsync(buscarTodosEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro durante o carregamento dos relatorios: {authContent}",
                authContent);

            return null;
        }

        var relatorioModel = JsonConvert.DeserializeObject<List<RelatorioModel>>(authContent);

        return relatorioModel;
    }
    public async Task<RelatorioModel?> BuscarPorId(Guid id)
    {
        string buscarPorIdEndpoint = _config["apiLocation"] + _config["buscarRelatorioPorId"] + $"/{id}";
        var authResult = await _client.GetAsync(buscarPorIdEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro durante o carregamento do relatorio por id: {authContent}",
                authContent);

            return null;
        }

        var relatorioModel = JsonConvert.DeserializeObject<RelatorioModel>(authContent);

        return relatorioModel;
    }
    public async Task<string?> Criar(RelatorioModel relatorio)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Descricao", relatorio.Descricao),
            new KeyValuePair<string, string>("NomeUsuario", relatorio.NomeUsuario ?? ""),
            new KeyValuePair<string, string>("NomeFerramenta", relatorio.NomeFerramenta ?? "")
        });

        string criarUsuarioEndpoint = _config["apiLocation"] + _config["criarRelatorio"];
        var authResult = await _client.PostAsync(criarUsuarioEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu para criar o relatorio: {authContent}",
                authContent);

            return authContent;
        }

        return null;
    }

    public async Task<string?> Atualizar(RelatorioModel relatorio)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Id", relatorio.Id.ToString()),
            new KeyValuePair<string, string>("Descricao", relatorio.Descricao),
            new KeyValuePair<string, string>("NomeUsuario", relatorio.NomeUsuario ?? ""),
            new KeyValuePair<string, string>("NomeFerramenta", relatorio.NomeFerramenta ?? "")
        });

        string atualizarUsuarioEndpoint = _config["apiLocation"] + _config["atualizarRelatorio"];
        var authResult = await _client.PutAsync(atualizarUsuarioEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro ao atualizar o relatorio: {authContent}",
                authContent);

            return await authResult.Content.ReadAsStringAsync(); ;
        }

        return null;
    }
    public async Task<string?> Deletar(Guid id)
    {
        string deletarRelatorioEndpoint = _config["apiLocation"] + _config["deletarRelatorio"] + $"/{id}";
        var authResult = await _client.DeleteAsync(deletarRelatorioEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogError("Ocorreu um erro para deletar o relatorio: {authContent}",
                authContent);

            return await authResult.Content.ReadAsStringAsync(); ;
        }

        return null;
    }
}
