using SoftwareControle.Models;

namespace SoftwareControle.Site.APICall.Ordem
{
    public interface IOrdemEndpoints
    {
        Task<string?> Atualizar(OrdemModel ordem);
        Task<List<OrdemModel>?> Buscar();
        Task<OrdemModel?> BuscarPorId(Guid id);
        Task<string?> Criar(OrdemModel ordem);
        Task<string?> Deletar(Guid id);
        Task<List<OrdemModel>?> BuscarPorNomeResponsavel(string nomeResponsavel);
    }
}