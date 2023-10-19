using SoftwareControle.Site.Models;

namespace SoftwareControle.Site.Generator
{
    public interface IRelatorioGenerator
    {
        RelatorioModel AceitarOrdemRelatorio(string nomeUsuario, string nomeFerramenta,
            string ordemDescricao);
        RelatorioModel FinalizarOrdemRelatorio(string nomeUsuario, string nomeFerramenta,
            string ordemDescricao);
        RelatorioModel CriarFerramentaRelatorio(string nomeUsuario, string nomeFerramenta);
	}
}