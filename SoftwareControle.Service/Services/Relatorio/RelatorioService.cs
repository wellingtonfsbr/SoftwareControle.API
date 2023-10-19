using FluentValidation;
using SoftwareControle.Models;
using SoftwareControle.Repositorio.Repositorio.Relatorio;
using SoftwareControle.Services.Services.Usuario;

namespace SoftwareControle.Service.Services.Relatorio;

public class RelatorioService : IRelatorioService
{
    private readonly IRelatorioRepositorio _relatorioRepositorio;
    private readonly IValidator<RelatorioModel> _relatorioValidator;

    public RelatorioService(IRelatorioRepositorio relatorioRepositorio,
        IValidator<RelatorioModel> relatorioValidator)
    {
        _relatorioRepositorio = relatorioRepositorio;
        _relatorioValidator = relatorioValidator;
    }

    public async Task<List<RelatorioModel>?> Buscar(CancellationToken cancellationToken)
    {
        return await _relatorioRepositorio.Buscar(cancellationToken);
    }

    public async Task<RelatorioModel?> Buscar(Guid id, CancellationToken cancellationToken)
    {
        return await _relatorioRepositorio.Buscar(id, cancellationToken);
    }

    public async Task<string?> Adicionar(RelatorioModel relatorio, CancellationToken cancellationToken)
    {
        var resultado = _relatorioValidator.Validate(relatorio);

        if (!resultado.IsValid)
            return resultado.Errors.FirstOrDefault()!.ToString();

        relatorio.Id = Guid.NewGuid();
        relatorio.DataCriacao = DateTime.UtcNow.AddHours(-3);

        await _relatorioRepositorio.Adicionar(relatorio, cancellationToken);

        return null;
    }

    public async Task<bool> Atualizar(RelatorioModel relatorio, CancellationToken cancellationToken)
    {
        return await _relatorioRepositorio.Atualizar(relatorio, cancellationToken);
    }
    public async Task<bool> Deletar(Guid id, CancellationToken cancellationToken)
    {
        return await _relatorioRepositorio.Deletar(id, cancellationToken);
    }
}
