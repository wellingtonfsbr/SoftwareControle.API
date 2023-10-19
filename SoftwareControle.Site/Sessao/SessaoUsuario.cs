using Microsoft.AspNetCore.Components.Authorization;
using SoftwareControle.Website.Session;
using System.Security.Claims;

namespace SoftwareControle.Site.Sessao;

public class SessaoUsuario : ISessaoUsuario
{
	private readonly AuthenticationStateProvider _authenticationStateProvider;

	public SessaoUsuario(AuthenticationStateProvider authenticationStateProvider)

	{
		_authenticationStateProvider = authenticationStateProvider;
	}

	public async Task<Guid> BuscarIdDoUsuarioLogado()
	{
		var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
		var user = authenticationState.User;
		var idUsuario = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (idUsuario is null)
		{
			return Guid.Empty;
		}

		return new Guid(idUsuario);
	}
}
