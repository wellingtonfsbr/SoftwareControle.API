using Application.Services.Auth;
using HFAcademia.API.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoftwareControle.API.Mapper;

namespace SoftwareControle.API.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost, AllowAnonymous, Route("/login")]
    [ProducesResponseType(typeof(string), 200),
    ProducesResponseType(404)]
    public async Task<IActionResult> Login([FromForm] LoginRequest userLogin,
        CancellationToken cancellationToken)
    {
        var loginResult = await _authService.Login(userLogin.MapLoginRequestToDomainModel(), cancellationToken);

        if (loginResult is null)
            return NotFound("Usuario ou senha incorretos");

        var output = new
        {
            Access_Token = loginResult.Value.Item1,
            Username = loginResult.Value.Item2
        };

        return Ok(output);
    }
}
