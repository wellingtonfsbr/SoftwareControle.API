namespace SoftwareControle.API.Requests;

public class FerramentaRequest
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public bool Disponivel { get; set; }
    public string? ImagemString { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }

    public Guid UsuarioId { get; set; }

}
