using SoftwareControle.Services.Services.Usuario;
using Microsoft.AspNetCore.Mvc;
using SoftwareControle.Models;
using Microsoft.AspNetCore.Authorization;

namespace SoftwareControle.API.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class RelatorioController : ControllerBase
{
    private readonly IRelatorioService _relatorioService;

    public RelatorioController(IRelatorioService relatorioService)
    {
        _relatorioService = relatorioService;
    }

    [ProducesResponseType(typeof(List<RelatorioModel>),200)]
    [HttpGet, Route("/relatorio/buscar")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var relatorios = await _relatorioService.Buscar(cancellationToken);

        if (relatorios is null)
            return NotFound("Não foi encontrado nenhum relatorio");

        return Ok(relatorios);
    }

    [HttpGet, Route("/relatorio/buscarporid/{id:guid}"), Authorize]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var relatorio = await _relatorioService.Buscar(id, cancellationToken);

        if (relatorio is null)
            return NotFound("Não foi encontrado nenhum relatorio");

        return Ok(relatorio);
    }

    [HttpPost, Route("/relatorio/criar"), Authorize]
    public async Task<ActionResult<string?>> Post([FromForm] RelatorioModel relatorioRequest,
        CancellationToken cancellationToken)
    {
        var resultado = await _relatorioService.Adicionar(relatorioRequest, cancellationToken);

        if (resultado is not null)
            return BadRequest(resultado);

        return Ok(null);
    }

    [HttpPut, Route("/relatorio/atualizar"), Authorize]
    public async Task<IActionResult> Put([FromForm] RelatorioModel relatorioRequest,
        CancellationToken cancellationToken)
    {
        bool relatorioFoiAtualizado = await _relatorioService.Atualizar(relatorioRequest,
            cancellationToken);

        if (relatorioFoiAtualizado is false)
            return NotFound("Não foi possivel atualizar o relatorio");

        return Ok(null);
    }

    [HttpDelete, Route("/relatorio/deletar/{id:guid}"), Authorize]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        bool relatorioFoiDeletado = await _relatorioService.Deletar(id, cancellationToken);

        if (relatorioFoiDeletado is false)
            return NotFound("Não foi possivel deletar o relatorio");

        return Ok("Relatorio deletado com sucesso");
    }
}
