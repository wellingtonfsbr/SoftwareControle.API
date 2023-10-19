using FluentValidation;
using SoftwareControle.Models;
using SoftwareControle.Repository.Repositorio.Ordem;

namespace SoftwareControle.Services.Services.Usuario;

public class OrdemService : IOrdemService
{
	private readonly IOrdemRepositorio _ordemRepositorio;
    private readonly IValidator<OrdemModel> _ordemValidator;


    public OrdemService(IOrdemRepositorio ordemRepositorio, IValidator<OrdemModel> ordemValidator)
    {
        _ordemRepositorio = ordemRepositorio;
        _ordemValidator = ordemValidator;
    }

    public async Task<List<OrdemModel>?> Buscar(CancellationToken cancellationToken)
	{
		return await _ordemRepositorio.Buscar(cancellationToken);
	}
	public async Task<OrdemModel?> Buscar(Guid id, CancellationToken cancellationToken)
	{
		return await _ordemRepositorio.Buscar(id, cancellationToken);
	}
	public async Task<string?> Adicionar(OrdemModel ordem, CancellationToken cancellationToken)
	{
        var resultado = _ordemValidator.Validate(ordem);

        if (!resultado.IsValid)
            return resultado.Errors.FirstOrDefault()!.ToString();

		if (ordem.DataPrazoMaximo < DateTime.Today)
			return "Não é possivel adicionar uma data de prazo maximo que não seja futura";

		ordem.Id = Guid.NewGuid();
		ordem.DataCriacao = DateTime.UtcNow.AddHours(-3);

        await _ordemRepositorio.Adicionar(ordem, cancellationToken);

		return null;
	}
	public async Task<bool> Atualizar(OrdemModel ordem, CancellationToken cancellationToken)
	{
		return await _ordemRepositorio.Atualizar(ordem, cancellationToken);
	}
	public async Task<bool> Deletar(Guid id, CancellationToken cancellationToken)
	{
		return await _ordemRepositorio.Deletar(id, cancellationToken);
	}
    public async Task<List<OrdemModel>?> BuscarPorNomeResponsavel(string nomeResponsavel,
		CancellationToken cancellationToken)
    {
        return await _ordemRepositorio.BuscarPorNomeResponsavel(nomeResponsavel, cancellationToken);
    }
}
