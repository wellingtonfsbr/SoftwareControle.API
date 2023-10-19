using SoftwareControle.Models;

namespace SoftwareControle.Services.Services.Usuario
{
	public interface IUsuarioService
	{
		Task<string?> Adicionar(UsuarioModel usuario, CancellationToken cancellationToken);
		Task<bool> Atualizar(UsuarioModel user, CancellationToken cancellationToken);
		Task<List<UsuarioModel>?> Buscar(CancellationToken cancellationToken);
		Task<UsuarioModel?> Buscar(Guid id, CancellationToken cancellationToken);
		Task<bool> Deletar(Guid id, CancellationToken cancellationToken);
	}
}