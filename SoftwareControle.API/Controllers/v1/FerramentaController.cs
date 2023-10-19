using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareControle.API.Mapper;
using SoftwareControle.API.Requests;
using SoftwareControle.Services.Services.Usuario;

namespace SoftwareControle.API.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class FerramentaController : ControllerBase
{
    private readonly IFerramentaService _ferramentaService;
    private readonly ILogger<FerramentaController> _logger;

	public FerramentaController(IFerramentaService ferramentaService,
        ILogger<FerramentaController> logger)
	{
		_ferramentaService = ferramentaService;
		_logger = logger;
	}

	[HttpGet, Route("/ferramenta/buscar"), Authorize]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {

		var	ferramentas = await _ferramentaService.Buscar(cancellationToken);

        if (ferramentas!.Count == 0)
			return NotFound("Não foi encontrado nenhuma ferramenta");

		return Ok(ferramentas);
    }

    [HttpGet, Route("/ferramenta/buscarporid/{id:guid}"), Authorize]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var ferramenta = await _ferramentaService.Buscar(id, cancellationToken);

        if (ferramenta is null)
            return NotFound("Não foi encontrado nenhuma ferramenta");

        return Ok(ferramenta);
    }

    [HttpPost, Route("/ferramenta/criar"), Authorize]
    public async Task<ActionResult<string?>> Post([FromForm] FerramentaRequest ferramentaRequest,
        CancellationToken cancellationToken)
    {
        var resultado = await _ferramentaService
            .Adicionar(ferramentaRequest.MapFerramentaRequestToFerramentaModel(),cancellationToken);

        if (resultado is not null)
            return BadRequest(resultado);

        return Ok(null);
    }

    [HttpPut, Route("/ferramenta/atualizar"), Authorize]
    public async Task<IActionResult> Put([FromForm] FerramentaRequest ferramentaRequest,
        CancellationToken cancellationToken)
    {
        bool ferramentaFoiAtualizada = await _ferramentaService
            .Atualizar(ferramentaRequest.MapFerramentaRequestToFerramentaModel(), cancellationToken);

        if (ferramentaFoiAtualizada is false)
            return NotFound("Ocorreu um erro ao atualizar os dados da ferramenta");

        return Ok(null);
    }

    [HttpDelete, Route("/ferramenta/deletar/{id:guid}"), Authorize]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        bool ferramentaFoiDeletada = await _ferramentaService.Deletar(id, cancellationToken);

        if (ferramentaFoiDeletada is false)
            return NotFound("Não foi possivel deletar a ferramenta");

        return Ok("Ferramenta deletada com sucesso");
    }
}
