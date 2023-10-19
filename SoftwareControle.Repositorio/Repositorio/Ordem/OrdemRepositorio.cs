using Microsoft.EntityFrameworkCore;
using SoftwareControle.Models;
using SoftwareControle.Repositorio.Context;

namespace SoftwareControle.Repository.Repositorio.Ordem;

public class OrdemRepositorio : IOrdemRepositorio
{
	private readonly ApplicationDbContext _context;

	public OrdemRepositorio(ApplicationDbContext context)
	{
		_context = context;
	}
	public async Task<List<OrdemModel>?> Buscar(CancellationToken cancellationToken)
	{
        List<OrdemModel>? ordens = await _context.Ordens.ToListAsync(cancellationToken);

        return ordens is not null ? ordens : null;
	}
	public async Task<OrdemModel?> Buscar(Guid id, CancellationToken cancellationToken)
	{
		OrdemModel? ordem = await _context.Ordens.SingleOrDefaultAsync
			(u => u.Id == id, cancellationToken);

		return ordem is not null ? ordem : null;
	}
	public async Task Adicionar(OrdemModel ordem, CancellationToken cancellationToken)
	{
		await _context.Ordens.AddAsync(ordem, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
	}
	public async Task<bool> Atualizar(OrdemModel ordem, CancellationToken cancellationToken)
	{
		OrdemModel? requestedOrdem = await _context.Ordens.SingleOrDefaultAsync
			(u => u.Id == ordem.Id, cancellationToken);

		if (requestedOrdem is null)
			return false;

		requestedOrdem.Descricao = ordem.Descricao;
		requestedOrdem.NivelUrgencia = ordem.NivelUrgencia;
		requestedOrdem.Situacao = ordem.Situacao;
		requestedOrdem.DataPrazoMaximo = ordem.DataPrazoMaximo;
		requestedOrdem.DataAtualizacao = DateTime.UtcNow.AddHours(-3);
		requestedOrdem.NomeResponsavel = ordem.NomeResponsavel;
		requestedOrdem.DataIniciado = ordem.DataIniciado;
		requestedOrdem.DataFinalizado = ordem.DataFinalizado;
		requestedOrdem.DescricaoResponsavel = ordem.DescricaoResponsavel;
		requestedOrdem.HorasTrabalhadas = ordem.HorasTrabalhadas;

        _context.Ordens.Update(requestedOrdem);
		await _context.SaveChangesAsync(cancellationToken);

		return true;
	}
	public async Task<bool> Deletar(Guid id, CancellationToken cancellationToken)
	{
		int colunasAfetadas = await _context.Ordens.Where(u => u.Id == id)
									.ExecuteDeleteAsync(cancellationToken);

		await _context.SaveChangesAsync(cancellationToken);

		return colunasAfetadas > 0;
	}
    public async Task<List<OrdemModel>?> BuscarPorNomeResponsavel(string nomeResponsavel, 
		CancellationToken cancellationToken)
    {
        List<OrdemModel>? ordens = await _context.Ordens
			.Where(u => u.NomeResponsavel == nomeResponsavel)
			.ToListAsync(cancellationToken);

        return ordens is not null ? ordens : null;
    }
}
