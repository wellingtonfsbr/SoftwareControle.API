using SoftwareControle.API.Response;
using SoftwareControle.Models;
using System.Collections;

namespace SoftwareControle.API.Mapper;

public static class UsuarioModelToUsuarioResponse
{
    public static UsuarioResponse MapUsuarioModelToResponse(this UsuarioModel usuarioRequest)
	{
        string stringImagem = string.Empty;

        if (usuarioRequest.Imagem is not null)
        {
			stringImagem = Convert.ToBase64String(usuarioRequest.Imagem);
		}

        return new UsuarioResponse()
        {
            Id = usuarioRequest.Id,
            Nome = usuarioRequest.Nome,
            Usuario = usuarioRequest.Usuario,
            Cargo = usuarioRequest.Cargo,
            ImagemString = stringImagem,
            Imagem = usuarioRequest.Imagem,
            DataCriacao = usuarioRequest.DataCriacao,
            DataAtualizacao = usuarioRequest.DataAtualizacao,
            Ordem = usuarioRequest.Ordem,
            Ferramenta = usuarioRequest.Ferramenta
        };
    }
}
