using BarberBoss.Application.UseCases.User.Register;
using BarberBoss.Application.UseCases.User.RegisterAdministrator;
using BarberBoss.Communication.User.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [Route("team-member")]
    public async Task<IActionResult> RegisterTeamMember(
        [FromServices] IRegisterTeamMemberUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpPost]
    [Route("administrator")]
    public async Task<IActionResult> RegisterAdministrator(
        [FromServices] IRegisterUserAdministratorUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
