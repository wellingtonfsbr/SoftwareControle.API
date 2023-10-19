using HFAcademia.API.Requests;
using SoftwareControle.Models;

namespace SoftwareControle.API.Mapper;

public static class LoginRequestToUsuarioModel
{
    public static UsuarioModel MapLoginRequestToDomainModel(this LoginRequest loginRequest)
    {
        return new UsuarioModel
        {
            Usuario = loginRequest.Usuario,
            Senha = loginRequest.Senha
        };
    }
}
