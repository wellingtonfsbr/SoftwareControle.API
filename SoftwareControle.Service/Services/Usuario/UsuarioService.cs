using FluentValidation;
using SoftwareControle.Models;
using SoftwareControle.Repository.Repositorio.Usuario;
using System.Formats.Asn1;

namespace SoftwareControle.Services.Services.Usuario;

public class UsuarioService : IUsuarioService
{
	private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly IValidator<UsuarioModel> _usuarioValidator;


    public UsuarioService(IUsuarioRepositorio usuarioRepositorio,
		IValidator<UsuarioModel> usuarioValidator)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _usuarioValidator = usuarioValidator;
    }

    public async Task<List<UsuarioModel>?> Buscar(CancellationToken cancellationToken)
	{
		return await _usuarioRepositorio.Buscar(cancellationToken);
	}

	public async Task<UsuarioModel?> Buscar(Guid id, CancellationToken cancellationToken)
	{
		return await _usuarioRepositorio.Buscar(id, cancellationToken);
	}

	public async Task<string?> Adicionar(UsuarioModel usuario, CancellationToken cancellationToken)
	{
        var resultado = _usuarioValidator.Validate(usuario);

        if (!resultado.IsValid)
            return resultado.Errors.FirstOrDefault()!.ToString();

		var nomeJaUtilizado = await VerificarNomeUsuario(usuario, cancellationToken);
		if (nomeJaUtilizado is not null)
			return "Este nome ja esta cadastrado no sistema";

		var usuarioLoginJaUtilizado = await VerificarNomeUsuarioLogin(usuario, cancellationToken);
		if (usuarioLoginJaUtilizado is not null)
			return "Um usuario com este login de usuario ja foi cadastrado";

		usuario.Id = Guid.NewGuid();
		usuario.DataCriacao = DateTime.UtcNow.AddHours(-3);

		await _usuarioRepositorio.Adicionar(usuario, cancellationToken);

		return null;
	}

	public async Task<bool> Atualizar(UsuarioModel user, CancellationToken cancellationToken)
	{
		return await _usuarioRepositorio.Atualizar(user, cancellationToken);
	}
	public async Task<bool> Deletar(Guid id, CancellationToken cancellationToken)
	{
		return await _usuarioRepositorio.Deletar(id, cancellationToken);
	}
	
	private async Task<UsuarioModel?> VerificarNomeUsuario(UsuarioModel usuario, CancellationToken ct)
	{
		var usuarioRequested = await _usuarioRepositorio.BuscarPorNome(usuario.Nome, ct);

		if(usuarioRequested is null)
			return null;

		return usuarioRequested;
	}

	private async Task <UsuarioModel?> VerificarNomeUsuarioLogin(UsuarioModel usuario, CancellationToken ct)
	{
		var usuarioRequested = await _usuarioRepositorio.BuscarPorUsuarioLogin(usuario.Usuario, ct);

		if (usuarioRequested is null)
			return null;

		return usuarioRequested;
	}
}
