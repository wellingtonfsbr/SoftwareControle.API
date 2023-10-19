namespace SoftwareControle.Models;

public class FerramentaModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public bool Disponivel { get; set; }
    public byte[]? Imagem { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    
    public Guid UsuarioId { get; set; }

    // Propriedades de navegação
    public List<OrdemModel>? Ordem { get; set; }
    public UsuarioModel? Usuario { get; set; }
}
