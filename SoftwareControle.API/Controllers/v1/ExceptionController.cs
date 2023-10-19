using Microsoft.AspNetCore.Mvc;

namespace SoftwareControle.API.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class ExceptionController : ControllerBase
{
    [HttpGet, Route("/exception/throw")]
    public IActionResult ThrowException()
    {
		try
		{
			throw new Exception("Exception thrown");
		}
		catch (Exception ex)
		{
			return StatusCode(500, "An error occurred: " + ex.Message);
		}
	}

    [HttpGet, Route("/ok")]
    public IActionResult OkResult()
    {
		return Ok("Request successfull");
    }
}
