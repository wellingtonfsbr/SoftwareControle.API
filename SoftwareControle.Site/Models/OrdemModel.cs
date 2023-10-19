using SoftwareControle.Site.Models;

namespace SoftwareControle.Models;

public class OrdemModel
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public string NivelUrgencia { get; set; } = string.Empty;
    public string Situacao { get; set; } = string.Empty;
    public string NomeFerramenta { get; set; } = string.Empty;
    public string? NomeResponsavel { get; set; } = string.Empty;
    public string? DescricaoResponsavel { get; set; } = string.Empty;
    public DateTime DataPrazoMaximo { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataIniciado { get; set; }
    public DateTime? DataFinalizado { get; set; }
    public DateTime? DataAtualizacao { get; set; }
	public int? HorasTrabalhadas { get; set; }

	//Chaves estrangeira
	public Guid UsuarioId { get; set; }
    public Guid FerramentaId { get; set; }

    public UsuarioModel? Usuario { get; set; }
    public FerramentaModel? Ferramenta { get; set; }
}