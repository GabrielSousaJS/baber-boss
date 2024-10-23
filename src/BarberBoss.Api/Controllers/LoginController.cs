using BarberBoss.Application.UseCases.Login.DoLogin;
using BarberBoss.Communication.Login.Request;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(
        [FromServices] IDoLoginUseCase useCase,
        [FromBody] RequestUserLoginJson request)
    {
        var result = await useCase.Execute(request);

        return Ok(result);
    }
}
