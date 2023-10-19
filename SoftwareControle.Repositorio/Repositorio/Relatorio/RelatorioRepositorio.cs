using Microsoft.EntityFrameworkCore;
using SoftwareControle.Models;
using SoftwareControle.Repositorio.Context;

namespace SoftwareControle.Repositorio.Repositorio.Relatorio;

public class RelatorioRepositorio : IRelatorioRepositorio
{
    private readonly ApplicationDbContext _context;

    public RelatorioRepositorio(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<List<RelatorioModel>?> Buscar(CancellationToken cancellationToken)
    {
        List<RelatorioModel>? relatorios = await _context.Relatorio.ToListAsync(cancellationToken);

        return relatorios is not null ? relatorios : null;
    }
    public async Task<RelatorioModel?> Buscar(Guid id, CancellationToken cancellationToken)
    {
        RelatorioModel? relatorio = await _context.Relatorio.SingleOrDefaultAsync
            (u => u.Id == id, cancellationToken);

        return relatorio is not null ? relatorio : null;
    }
    public async Task Adicionar(RelatorioModel relatorio, CancellationToken cancellationToken)
    {
        await _context.Relatorio.AddAsync(relatorio, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<bool> Atualizar(RelatorioModel relatorio, CancellationToken cancellationToken)
    {
        OrdemModel? requestedRelatorio = await _context.Ordens.SingleOrDefaultAsync
            (u => u.Id == relatorio.Id, cancellationToken);

        if (requestedRelatorio is null)
            return false;


        _context.Ordens.Update(requestedRelatorio);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
    public async Task<bool> Deletar(Guid id, CancellationToken cancellationToken)
    {
        int colunasAfetadas = await _context.Relatorio.Where(u => u.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return colunasAfetadas > 0;
    }
}
