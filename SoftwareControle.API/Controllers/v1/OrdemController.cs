using SoftwareControle.Services.Services.Usuario;
using Microsoft.AspNetCore.Mvc;
using SoftwareControle.Models;
using Microsoft.AspNetCore.Authorization;

namespace SoftwareControle.API.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class OrdemController : ControllerBase
{
    private readonly IOrdemService _ordemService;

    public OrdemController(IOrdemService ordemService)
    {
        _ordemService = ordemService;
    }

    [ProducesResponseType(typeof(List<OrdemModel>),200)]
    [HttpGet, Route("/ordem/buscar")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var ordens = await _ordemService.Buscar(cancellationToken);

        if (ordens is null || ordens.Count == 0)
            return NotFound("Não foi encontrado nenhuma ordem");

        return Ok(ordens);
    }

    [HttpGet, Route("/ordem/buscarporid/{id:guid}"), Authorize]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var ordem = await _ordemService.Buscar(id, cancellationToken);

        if (ordem is null)
            return NotFound("Não foi encontrado nenhuma ordem");

        return Ok(ordem);
    }

    [HttpPost, Route("/ordem/criar"), Authorize]
    public async Task<ActionResult<string?>> Post([FromForm] OrdemModel ordemRequest,
        CancellationToken cancellationToken)
    {
        var resultado = await _ordemService.Adicionar(ordemRequest, cancellationToken);

        if (resultado is not null)
            return BadRequest(resultado);

        return Ok(null);
    }

    [HttpPut, Route("/ordem/atualizar"), Authorize]
    public async Task<IActionResult> Put([FromForm] OrdemModel ordemRequest,
        CancellationToken cancellationToken)
    {
        bool ordemFoiAtualizada = await _ordemService.Atualizar(ordemRequest,
            cancellationToken);

        if (ordemFoiAtualizada is false)
            return NotFound("Não foi possivel atualizar a ordem");

        return Ok(null);
    }

    [HttpDelete, Route("/ordem/deletar/{id:guid}"), Authorize]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        bool ordemFoiDeletada = await _ordemService.Deletar(id, cancellationToken);

        if (ordemFoiDeletada is false)
            return NotFound("Não foi possivel deletar a ordem");

        return Ok("Ordem deletada com sucesso");
    }

    [HttpGet, Route("/ordem/buscarpornomeresponsavel/{nomeResponsavel}"), Authorize]
    public async Task<IActionResult> Get(string nomeResponsavel, CancellationToken cancellationToken)
    {
        List<OrdemModel>? listaOrdens = new();

        if(nomeResponsavel is not null)
        {
            listaOrdens = await _ordemService.BuscarPorNomeResponsavel(nomeResponsavel,
                cancellationToken);
        }

        if (listaOrdens is null)
            return NotFound("Não foi encontrada nenhuma ordem por responsavel");

        return Ok(listaOrdens);
    }
}
