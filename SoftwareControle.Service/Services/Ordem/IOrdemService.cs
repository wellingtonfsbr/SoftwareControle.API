using SoftwareControle.Models;

namespace SoftwareControle.Services.Services.Usuario
{
	public interface IOrdemService
	{
		Task<string?> Adicionar(OrdemModel ordem, CancellationToken cancellationToken);
		Task<bool> Atualizar(OrdemModel ordem, CancellationToken cancellationToken);
		Task<List<OrdemModel>?> Buscar(CancellationToken cancellationToken);
		Task<OrdemModel?> Buscar(Guid id, CancellationToken cancellationToken);
		Task<bool> Deletar(Guid id, CancellationToken cancellationToken);
		Task<List<OrdemModel>?> BuscarPorNomeResponsavel(string nomeResponsavel,
		CancellationToken cancellationToken);
    }
}