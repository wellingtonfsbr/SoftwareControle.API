using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareControle.API.Mapper;
using SoftwareControle.API.Requests;
using SoftwareControle.API.Response;
using SoftwareControle.Services.Services.Usuario;

namespace SoftwareControle.API.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly ILogger<UsuarioController> _logger;

	public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> logger)
	{
		_usuarioService = usuarioService;
		_logger = logger;
	}

	[HttpGet, Route("/usuario/buscar"), Authorize]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var usuarios = await _usuarioService.Buscar(cancellationToken);

        List<UsuarioResponse> usuariosResponse = new();

        if (usuarios is null)
			return NotFound("Não foi encontrado nenhum usuario");

		foreach (var usuario in usuarios)
        {
            var usuarioResponse = usuario.MapUsuarioModelToResponse();
            usuariosResponse.Add(usuarioResponse);
        }

        return Ok(usuariosResponse);
    }

    [HttpGet, Route("/usuario/buscarporid/{id:guid}"), Authorize]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioService.Buscar(id, cancellationToken);

        if (usuario is null)
            return NotFound("Não foi encontrado nenhum usuario");

        UsuarioResponse? usuarioResponse = usuario.MapUsuarioModelToResponse();

        return Ok(usuarioResponse);
    }

    [HttpPost, Route("/usuario/criar"), Authorize]
    public async Task<ActionResult<string?>> Post([FromForm] UsuarioRequest usuarioRequest,
        CancellationToken cancellationToken)
    {
        var resultado = await _usuarioService.Adicionar(usuarioRequest.MapUsuarioRequestToUsuarioModel(),
            cancellationToken);

        if (resultado is not null)
            return BadRequest(resultado);

        return Ok(null);
    }

    [HttpPut, Route("/usuario/atualizar")]
    public async Task<IActionResult> Put([FromForm] UsuarioRequest usuarioRequest,
        CancellationToken cancellationToken)
    {
        bool usuarioFoiAtualizado = await _usuarioService.Atualizar(usuarioRequest.MapUsuarioRequestToUsuarioModel(),
            cancellationToken);

        if (usuarioFoiAtualizado is false)
            return NotFound("Não foi possivel atualizar o usuario");

        return Ok(null);
    }

    [HttpDelete, Route("/usuario/deletar/{id:guid}"), Authorize]

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        bool usuarioFoiDeletado = await _usuarioService.Deletar(id, cancellationToken);

        if (usuarioFoiDeletado is false)
            return NotFound("Não foi possivel deletar o usuario");

        return Ok("Usuario deletado com sucesso");
    }
}
