using SoftwareControle.Site.Models;

namespace SoftwareControle.Site.Generator;

public class RelatorioGenerator : IRelatorioGenerator
{
    public RelatorioModel AceitarOrdemRelatorio(string nomeUsuario,
        string nomeFerramenta, string ordemDescricao)
    {
        return new RelatorioModel
        {
            Descricao = $"O usuario {nomeUsuario} aceitou a ordem {ordemDescricao} " +
                $"Ferramenta: {nomeFerramenta} no dia {DateTime.UtcNow.AddHours(-3)}.",
            NomeFerramenta = nomeFerramenta,
            NomeUsuario = nomeUsuario
        };
    }
    public RelatorioModel FinalizarOrdemRelatorio(string nomeUsuario,
        string nomeFerramenta, string ordemDescricao)
    {
        return new RelatorioModel
        {
            Descricao = $"O usuario {nomeUsuario} finalizou a ordem {ordemDescricao} " +
                $"Ferramenta: {nomeFerramenta} no dia {DateTime.UtcNow.AddHours(-3)}.",
            NomeFerramenta = nomeFerramenta,
            NomeUsuario = nomeUsuario
        };
    }
	public RelatorioModel CriarFerramentaRelatorio(string nomeUsuario, string nomeFerramenta)
	{
		return new RelatorioModel
		{
			Descricao = $"O usuario {nomeUsuario} adicionou a ferramenta " +
		        $"{nomeFerramenta} ao sistema",
		    NomeFerramenta = nomeFerramenta,
			NomeUsuario = nomeUsuario
		};
	}
}
