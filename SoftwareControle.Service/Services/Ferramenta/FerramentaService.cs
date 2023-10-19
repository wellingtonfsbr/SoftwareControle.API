using FluentValidation;
using SoftwareControle.Models;
using SoftwareControle.Repository.Repositorio.Ordem;
using SoftwareControle.Services.Services.Usuario;

namespace SoftwareControle.Service.Services.Ferramenta;

public class FerramentaService : IFerramentaService
{
    private readonly IFerramentaRepositorio _ferramentaRepositorio;
    private readonly IValidator<FerramentaModel> _ferramentaValidator;


    public FerramentaService(IFerramentaRepositorio ferramentaRepositorio,
        IValidator<FerramentaModel> ferramentaValidator)
    {
        _ferramentaRepositorio = ferramentaRepositorio;
        _ferramentaValidator = ferramentaValidator;
    }

    public async Task<List<FerramentaModel>?> Buscar(CancellationToken cancellationToken)
    {
        return await _ferramentaRepositorio.Buscar(cancellationToken);
    }

    public async Task<FerramentaModel?> Buscar(Guid id, CancellationToken cancellationToken)
    {
        return await _ferramentaRepositorio.Buscar(id, cancellationToken);
    }

    public async Task<string?> Adicionar(FerramentaModel ferramenta, CancellationToken cancellationToken)
    {
        var resultado = _ferramentaValidator.Validate(ferramenta);

        if (!resultado.IsValid)
            return resultado.Errors.FirstOrDefault()!.ToString();

        string? nomeJaCadastrado = await VerificarNomeFerramenta(ferramenta, cancellationToken);

        if (nomeJaCadastrado is not null)
            return nomeJaCadastrado;

        ferramenta.Id = Guid.NewGuid();
        ferramenta.DataCriacao = DateTime.UtcNow.AddHours(-3);
        ferramenta.Disponivel = true;

        await _ferramentaRepositorio.Adicionar(ferramenta, cancellationToken);

        return null;
    }

    public async Task<bool> Atualizar(FerramentaModel ferramenta, CancellationToken cancellationToken)
    {
        ferramenta.DataAtualizacao = DateTime.UtcNow.AddHours(-3);

        return await _ferramentaRepositorio.Atualizar(ferramenta, cancellationToken);
    }
    public async Task<bool> Deletar(Guid id, CancellationToken cancellationToken)
    {
        return await _ferramentaRepositorio.Deletar(id, cancellationToken);
    }

    private async Task<string?> VerificarNomeFerramenta(FerramentaModel ferramenta, CancellationToken ct)
    {
        var ferramentaSolicitada = await _ferramentaRepositorio.BuscarPorNome(ferramenta.Nome, ct);

        if (ferramentaSolicitada is null)
            return null;

        return "O nome desta ferramenta ja esta cadastrado no sistema";
    }
}
