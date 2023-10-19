using Microsoft.EntityFrameworkCore;
using SoftwareControle.Models;
using SoftwareControle.Repositorio.Context;
using SoftwareControle.Repository.Repositorio.Usuario;

namespace SoftwareControle.Repositório;

public class UsuarioRepositorio : IUsuarioRepositorio
{
	private readonly ApplicationDbContext _context;

    public UsuarioRepositorio(ApplicationDbContext context)
	{
		_context = context;
	}
	public async Task<List<UsuarioModel>?> Buscar(CancellationToken cancellationToken)
	{
		List<UsuarioModel>? users = await _context.Usuarios.ToListAsync(cancellationToken);

		return users is not null ? users : null;
	}
	public async Task<UsuarioModel?> Buscar(Guid id, CancellationToken cancellationToken)
	{
		UsuarioModel? usuario = await _context.Usuarios.SingleOrDefaultAsync
			(u => u.Id == id, cancellationToken);

		return usuario is not null ? usuario : null;
	}
	public async Task Adicionar(UsuarioModel usuario, CancellationToken cancellationToken)
	{ 
        await _context.Usuarios.AddAsync(usuario, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}
	public async Task<bool> Atualizar(UsuarioModel user, CancellationToken cancellationToken)
	{
		UsuarioModel? requestedUser = await _context.Usuarios.SingleOrDefaultAsync
			(u => u.Id == user.Id, cancellationToken);

		if (requestedUser is null)
			return false;

		requestedUser.Usuario = user.Usuario;
		requestedUser.Nome = user.Nome;
		requestedUser.Senha = user.Senha;
		requestedUser.Cargo = user.Cargo;
		requestedUser.Imagem = user.Imagem;

		requestedUser.DataAtualizacao = DateTime.UtcNow.AddHours(-3);

		_context.Usuarios.Update(requestedUser);
		await _context.SaveChangesAsync(cancellationToken);

		return true;
	}
	public async Task<bool> Deletar(Guid id, CancellationToken cancellationToken)
	{
		int colunasAfetadas = await _context.Usuarios.Where(u => u.Id == id)
									.ExecuteDeleteAsync(cancellationToken);

		await _context.SaveChangesAsync(cancellationToken);

		return colunasAfetadas > 0;
	}

	public async Task<UsuarioModel?> BuscarPorNome(string nome, CancellationToken cancellationToken)
	{
		UsuarioModel? usuario = await _context.Usuarios.SingleOrDefaultAsync
			(u => u.Nome.ToLower() == nome.ToLower(), cancellationToken);

		return usuario is not null ? usuario : null;
	}

	public async Task<UsuarioModel?> BuscarPorUsuarioLogin(string usuarioLogin,
		CancellationToken cancellationToken)
	{
		UsuarioModel? usuario = await _context.Usuarios.SingleOrDefaultAsync
			(u => u.Usuario.ToLower() == usuarioLogin.ToLower(), cancellationToken);

		return usuario is not null ? usuario : null;
	}
}
