using SoftwareControle.Models;

namespace SoftwareControle.Repository.Repositorio.Usuario
{
    public interface IUsuarioRepositorio
    {
		Task<List<UsuarioModel>?> Buscar(CancellationToken cancellationToken);
		Task<UsuarioModel?> Buscar(Guid id, CancellationToken cancellationToken);
		Task Adicionar(UsuarioModel usuario, CancellationToken cancellationToken);
        Task<bool> Atualizar(UsuarioModel user, CancellationToken cancellationToken);
        Task<bool> Deletar(Guid id, CancellationToken cancellationToken);
		Task<UsuarioModel?> BuscarPorNome(string nome, CancellationToken cancellationToken);
		Task<UsuarioModel?> BuscarPorUsuarioLogin(string usuarioLogin, 
			CancellationToken cancellationToken);

	}
}