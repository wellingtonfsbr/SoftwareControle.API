namespace SoftwareControle.Site.Models;

public class RelatorioModel
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string? NomeFerramenta { get; set; } = string.Empty;
    public string? NomeUsuario { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
}
