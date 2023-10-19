using SoftwareControle.Site.Models;

namespace SoftwareControle.Models;
public class FerramentaModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public bool Disponivel { get; set; }
    public string? ImagemString { get; set; } = string.Empty;
    public byte[]? Imagem { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    //Chave estrangeira
    public Guid UsuarioId { get; set; }
    public UsuarioModel? Usuario { get; set; }
}
