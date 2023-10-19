using SoftwareControle.API.Requests;
using SoftwareControle.Models;

namespace SoftwareControle.API.Mapper;

public static class FerramentaRequestToFerramentaModel
{
    public static FerramentaModel MapFerramentaRequestToFerramentaModel
        (this FerramentaRequest ferramentaRequest)
    {
        byte[] byteArray = Convert.FromBase64String(ferramentaRequest.ImagemString ?? "");

        return new FerramentaModel
        {
            Id = ferramentaRequest.Id,
            Nome = ferramentaRequest.Nome,
            Descricao = ferramentaRequest.Descricao,
            Disponivel = ferramentaRequest.Disponivel,
            Imagem = byteArray,
            DataCriacao = ferramentaRequest.DataCriacao,
            DataAtualizacao = ferramentaRequest.DataAtualizacao,
            UsuarioId = ferramentaRequest.UsuarioId
        };
    }
}
