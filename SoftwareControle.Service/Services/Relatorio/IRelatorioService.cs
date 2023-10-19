using SoftwareControle.Models;

namespace SoftwareControle.Services.Services.Usuario
{
    public interface IRelatorioService
    {
        Task<string?> Adicionar(RelatorioModel relatorio, CancellationToken cancellationToken);
        Task<bool> Atualizar(RelatorioModel relatorio, CancellationToken cancellationToken);
        Task<List<RelatorioModel>?> Buscar(CancellationToken cancellationToken);
        Task<RelatorioModel?> Buscar(Guid id, CancellationToken cancellationToken);
        Task<bool> Deletar(Guid id, CancellationToken cancellationToken);
    }
}