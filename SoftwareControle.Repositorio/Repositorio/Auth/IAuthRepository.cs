using SoftwareControle.Models;

namespace HFAcademia.Repositório;

public interface IAuthRepository
{
    Task<UsuarioModel?> Login(UsuarioModel user, CancellationToken cancellationToken);
}