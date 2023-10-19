using HFAcademia.Repositório;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SoftwareControle.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Auth;

public class AuthService : IAuthService
{
	private readonly IAuthRepository _authRepository;
	private readonly IConfiguration _config;

	public AuthService(IAuthRepository authRepository, IConfiguration config)
	{
		_authRepository = authRepository;
		_config = config;
	}

	public async Task<(string, string)?> Login(UsuarioModel usuario, CancellationToken cancellationToken)
	{
		UsuarioModel? usuarioSolicitado = await _authRepository.Login(usuario, cancellationToken);

		if (usuarioSolicitado is null)
			return null;

		string token = GenerateToken(usuarioSolicitado);

		return (token, usuarioSolicitado.Nome);
	}

	private string GenerateToken(UsuarioModel usuario)
	{
		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var claims = new[]
		{
			new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
			new Claim(ClaimTypes.Name, usuario.Usuario),
			new Claim(ClaimTypes.Role, usuario.Cargo)
		};

		JwtSecurityToken token = new(_config["Jwt:Issuer"],
			_config["Jwt:Audience"],
			claims,
			expires: DateTime.UtcNow.AddDays(7),
			signingCredentials: credentials);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
