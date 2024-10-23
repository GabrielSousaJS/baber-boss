using BarberBoss.Application.UseCases.User.Profile;
using BarberBoss.Application.UseCases.User.Register;
using BarberBoss.Application.UseCases.User.RegisterAdministrator;
using BarberBoss.Application.UseCases.User.Update;
using BarberBoss.Communication.User.Requests;
using BarberBoss.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = Roles.ADMIN)]
    public async Task<IActionResult> RegisterAdministrator(
        [FromServices] IRegisterUserAdministratorUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [Route("profile")]
    [Authorize]
    public async Task<IActionResult> Profile(
        [FromServices] IUserProfileUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateUserUseCase useCase,
        [FromBody] RequestUpdateUserJson request)
    {
        await useCase.Execute(request);

        return NoContent();
    }
}
