using SoftwareControle.Repositorio.Context;
using Microsoft.EntityFrameworkCore;
using SoftwareControle.Models;

namespace SoftwareControle.Repository.Repositorio.Ordem;

public class FerramentaRepositorio : IFerramentaRepositorio
{
	private readonly ApplicationDbContext _context;

	public FerramentaRepositorio(ApplicationDbContext context)
	{
		_context = context;
	}
	public async Task<List<FerramentaModel>?> Buscar(CancellationToken cancellationToken)
	{
		List<FerramentaModel>? ferramentas = await _context.Ferramentas.ToListAsync(cancellationToken);

		return ferramentas is not null ? ferramentas : null;
	}
	public async Task<FerramentaModel?> Buscar(Guid id, CancellationToken cancellationToken)
	{
		FerramentaModel? ferramenta = await _context.Ferramentas.SingleOrDefaultAsync
			(u => u.Id == id, cancellationToken);

		return ferramenta is not null ? ferramenta : null;
	}
	public async Task Adicionar(FerramentaModel ferramentas, CancellationToken cancellationToken)
	{
		await _context.Ferramentas.AddAsync(ferramentas, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}
	public async Task<bool> Atualizar(FerramentaModel ferramenta, CancellationToken cancellationToken)
	{
		FerramentaModel? requestedFerramenta = await _context.Ferramentas.SingleOrDefaultAsync
			(u => u.Id == ferramenta.Id, cancellationToken);

		if (requestedFerramenta is null)
			return false;

		requestedFerramenta.Nome = ferramenta.Nome;
		requestedFerramenta.Descricao = ferramenta.Descricao;
		requestedFerramenta.Imagem = ferramenta.Imagem;
		requestedFerramenta.DataAtualizacao = ferramenta.DataAtualizacao;
		requestedFerramenta.Disponivel = ferramenta.Disponivel;

		_context.Ferramentas.Update(requestedFerramenta);
		await _context.SaveChangesAsync(cancellationToken);

		return true;
	}
	public async Task<bool> Deletar(Guid id, CancellationToken cancellationToken)
	{
		int colunasAfetadas = await _context.Ferramentas.Where(u => u.Id == id)
									.ExecuteDeleteAsync(cancellationToken);

		await _context.SaveChangesAsync(cancellationToken);

		return colunasAfetadas > 0;
	}
    public async Task<FerramentaModel?> BuscarPorNome(string nome, CancellationToken cancellationToken)
    {
        FerramentaModel? ferramenta = await _context.Ferramentas.SingleOrDefaultAsync
            (u => u.Nome == nome, cancellationToken);

        return ferramenta is not null ? ferramenta : null;
    }
}
