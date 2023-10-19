using SoftwareControle.Models;

namespace Application.Services.Auth
{
    public interface IAuthService
	{
		Task<(string, string)?> Login(UsuarioModel usuario, CancellationToken cancellationToken);
	}
}