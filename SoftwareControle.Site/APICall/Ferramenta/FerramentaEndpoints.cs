using Newtonsoft.Json;
using SoftwareControle.Models;

namespace SoftwareControle.Site.APICall.Ferramenta;

public class FerramentaEndpoints : IFerramentaEndpoints
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger<FerramentaEndpoints> _logger;

    public FerramentaEndpoints(HttpClient client,
    IConfiguration config,
    ILogger<FerramentaEndpoints> logger)
    {
        _client = client;
        _config = config;
        _logger = logger;
    }
    public async Task<List<FerramentaModel>?> Buscar()
    {
        string buscarTodosEndpoint = _config["apiLocation"] + _config["buscarFerramentas"];
        var authResult = await _client.GetAsync(buscarTodosEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro durante o carregamento das ferramentas: {authContent}",
                authContent);

            return null;
        }

        var ferramentaModel = JsonConvert.DeserializeObject<List<FerramentaModel>>(authContent);

        return ferramentaModel;
    }
    public async Task<FerramentaModel?> BuscarPorId(Guid id)
    {
        string buscarPorIdEndpoint = _config["apiLocation"] + _config["buscarFerramentaPorId"] + $"/{id}";
        var authResult = await _client.GetAsync(buscarPorIdEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro durante o carregamento da ferramenta por Id: {authContent}",
                authContent);

            return null;
        }

        var ferramentaModel = JsonConvert.DeserializeObject<FerramentaModel>(authContent);

        return ferramentaModel;
    }
    public async Task<string?> Criar(FerramentaModel ferramenta)
    {
        if(ferramenta.Imagem is not null)
        {
            ferramenta.ImagemString = Convert.ToBase64String(ferramenta.Imagem);
        }

        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Nome", ferramenta.Nome),
            new KeyValuePair<string, string>("Descricao", ferramenta.Descricao),
            new KeyValuePair<string, string>("ImagemString", ferramenta.ImagemString ?? ""),
            new KeyValuePair<string, string>("UsuarioId", ferramenta.UsuarioId.ToString())
        });

        string criarUsuarioEndpoint = _config["apiLocation"] + _config["criarFerramenta"];
        var authResult = await _client.PostAsync(criarUsuarioEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu para registrar a ferramenta: {authContent}",
                authContent);

            return authContent;
        }

        return null;
    }
    public async Task<string?> Atualizar(FerramentaModel ferramenta)
    {
        if (ferramenta.Imagem is not null)
        {
            ferramenta.ImagemString = Convert.ToBase64String(ferramenta.Imagem);
        }

        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Id", ferramenta.Id.ToString()),
            new KeyValuePair<string, string>("Nome", ferramenta.Nome),
            new KeyValuePair<string, string>("Descricao", ferramenta.Descricao),
            new KeyValuePair<string, string>("Disponivel", ferramenta.Disponivel.ToString()),
			new KeyValuePair<string, string>("ImagemString", ferramenta.ImagemString ?? "")
        });

        string atualizarEndpoint = _config["apiLocation"] + _config["atualizarFerramenta"];
        var authResult = await _client.PutAsync(atualizarEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro ao atualizar a ferramenta: {authContent}",
                authContent);

            return await authResult.Content.ReadAsStringAsync(); ;
        }

        return null;
    }
    public async Task<string?> Deletar(Guid id)
    {
        string deletarEndpoint = _config["apiLocation"] + _config["deletarFerramenta"] + $"/{id}";
        var authResult = await _client.DeleteAsync(deletarEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogError("Ocorreu um erro para deletar a ferramenta: {authContent}",
                authContent);

            return await authResult.Content.ReadAsStringAsync(); ;
        }

        return null;
    }
}
