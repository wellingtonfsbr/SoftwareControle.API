using Newtonsoft.Json;
using SoftwareControle.Site.Models;

namespace SoftwareControle.Site.APICall.Usuario;

public class UsuarioEndpoints : IUsuarioEndpoints
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger<UsuarioEndpoints> _logger;

    public UsuarioEndpoints(HttpClient client,
    IConfiguration config,
    ILogger<UsuarioEndpoints> logger)
    {
        _client = client;
        _config = config;
        _logger = logger;
    }
    public async Task<List<UsuarioModel>?> Buscar()
    {
        string buscarTodosEndpoint = _config["apiLocation"] + _config["bucarUsuarios"];
        var authResult = await _client.GetAsync(buscarTodosEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro durante o carregamento de usuarios: {authContent}",
                authContent);

            return null;
        }

        var userModel = JsonConvert.DeserializeObject<List<UsuarioModel>>(authContent);

        return userModel;
    }
    public async Task<UsuarioModel?> BuscarPorId(Guid id)
    {
        string buscarPorIdEndpoint = _config["apiLocation"] + _config["buscarUsuarioPorId"] + $"/{id}";
        var authResult = await _client.GetAsync(buscarPorIdEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro durante o carregamento do usuario por id: {authContent}",
                authContent);

            return null;
        }

        var userModel = JsonConvert.DeserializeObject<UsuarioModel>(authContent);

        return userModel;
    }
    public async Task<string?> Criar(UsuarioModel usuario)
    {
        if (usuario.Imagem is not null)
        {
            usuario.ImagemString = Convert.ToBase64String(usuario.Imagem);
        }

        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Usuario", usuario.Usuario),
            new KeyValuePair<string, string>("Senha", usuario.Senha),
            new KeyValuePair<string, string>("ImagemString", usuario.ImagemString ?? ""),
            new KeyValuePair<string, string>("Nome", usuario.Nome),
            new KeyValuePair<string, string>("Cargo", usuario.Cargo)
		});

        string criarEndpoint = _config["apiLocation"] + _config["criarUsuario"];
        var authResult = await _client.PostAsync(criarEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu para criar o usuario: {authContent}",
                authContent);

            return await authResult.Content.ReadAsStringAsync(); ;
        }

        return null;
    }

    public async Task<string?> Atualizar(UsuarioModel usuario)
    {
		if (usuario.Imagem is not null)
		{
			usuario.ImagemString = Convert.ToBase64String(usuario.Imagem);
		}

		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("Id", usuario.Id.ToString()),
			new KeyValuePair<string, string>("Usuario", usuario.Usuario),
			new KeyValuePair<string, string>("Nome", usuario.Nome),
			new KeyValuePair<string, string>("Cargo", usuario.Cargo),
			new KeyValuePair<string, string>("ImagemString", usuario.ImagemString ?? ""),
		});

		string atualizarEndpoint = _config["apiLocation"] + _config["atualizarUsuario"];
        var authResult = await _client.PutAsync(atualizarEndpoint, data);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger
                .LogError("Ocorreu um erro ao atualizar o usuario: {authContent}",
                authContent);

            return await authResult.Content.ReadAsStringAsync(); ;
        }

        return null;
    }
    public async Task<string?> Deletar(Guid id)
    {
        string deletarEndpoint = _config["apiLocation"] + _config["deletarUsuario"] + $"/{id}";
        var authResult = await _client.DeleteAsync(deletarEndpoint);
        var authContent = await authResult.Content.ReadAsStringAsync();

        if (authResult.IsSuccessStatusCode is false)
        {
            _logger.LogError("Ocorreu um erro para deletar o usuario: {authContent}",
                authContent);

            return await authResult.Content.ReadAsStringAsync();
        }

        return null;
    }

	public async Task<string?> AtualizarSenha(UsuarioModel usuario)
	{
		if (usuario.Imagem is not null)
		{
			usuario.ImagemString = Convert.ToBase64String(usuario.Imagem);
		}

		var data = new FormUrlEncodedContent(new[]
		{
			new KeyValuePair<string, string>("Id", usuario.Id.ToString()),
			new KeyValuePair<string, string>("Usuario", usuario.Usuario),
			new KeyValuePair<string, string>("Nome", usuario.Nome),
			new KeyValuePair<string, string>("Senha", usuario.Senha),
			new KeyValuePair<string, string>("Cargo", usuario.Cargo),
			new KeyValuePair<string, string>("ImagemString", usuario.ImagemString ?? ""),
		});

		string atualizarEndpoint = _config["apiLocation"] + _config["atualizarUsuario"];
		var authResult = await _client.PutAsync(atualizarEndpoint, data);
		var authContent = await authResult.Content.ReadAsStringAsync();

		if (authResult.IsSuccessStatusCode is false)
		{
			_logger
				.LogError("Ocorreu um erro ao atualizar a senha: {authContent}",
				authContent);

			return await authResult.Content.ReadAsStringAsync(); ;
		}

		return null;
	}
}
