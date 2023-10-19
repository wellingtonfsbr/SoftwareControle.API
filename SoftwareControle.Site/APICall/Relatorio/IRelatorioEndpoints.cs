using SoftwareControle.Site.Models;

namespace SoftwareControle.Site.APICall.Usuario
{
    public interface IRelatorioEndpoints
    {
        Task<string?> Atualizar(RelatorioModel relatorio);
        Task<List<RelatorioModel>?> Buscar();
        Task<RelatorioModel?> BuscarPorId(Guid id);
        Task<string?> Criar(RelatorioModel relatorio);
        Task<string?> Deletar(Guid id);
    }
}