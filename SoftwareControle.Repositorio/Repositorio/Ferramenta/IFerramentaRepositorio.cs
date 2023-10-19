using SoftwareControle.Models;

namespace SoftwareControle.Repository.Repositorio.Ordem
{
	public interface IFerramentaRepositorio
	{
		Task Adicionar(FerramentaModel ferramentas, CancellationToken cancellationToken);
		Task<bool> Atualizar(FerramentaModel ferramentas, CancellationToken cancellationToken);
		Task<List<FerramentaModel>?> Buscar(CancellationToken cancellationToken);
		Task<FerramentaModel?> Buscar(Guid id, CancellationToken cancellationToken);
		Task<bool> Deletar(Guid id, CancellationToken cancellationToken);
		Task<FerramentaModel?> BuscarPorNome(string nome, CancellationToken cancellationToken);

    }
}