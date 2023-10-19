using SoftwareControle.Models;

namespace SoftwareControle.Site.APICall.Ferramenta
{
    public interface IFerramentaEndpoints
    {
        Task<List<FerramentaModel>?> Buscar();
        Task<FerramentaModel?> BuscarPorId(Guid id);
        Task<string?> Criar(FerramentaModel ferramenta);
        Task<string?> Atualizar(FerramentaModel ferramenta);
        Task<string?> Deletar(Guid id);
    }
}