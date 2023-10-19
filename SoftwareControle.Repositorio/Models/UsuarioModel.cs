namespace SoftwareControle.Models;

public class UsuarioModel
{
    public Guid Id { get; set; } 
	public string Usuario { get; set; } = string.Empty;
	public string Senha { get; set; } = string.Empty;
	public string Nome { get; set; } = string.Empty;
    public string Cargo { get; set; } = string.Empty;
    public byte[]? Imagem { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    // Propriedades de navegação
    public List<OrdemModel>? Ordem { get; set; }
    public List<FerramentaModel>? Ferramenta { get; set; }
}
