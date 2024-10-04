using BarberBoss.Application.UseCases.Invoice.GetAll;
using BarberBoss.Application.UseCases.Invoice.GetById;
using BarberBoss.Application.UseCases.Invoice.Register;
using BarberBoss.Communication.Invoice.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterInvoiceUseCase useCase,
        [FromBody] RequestRegisterInvoiceJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromServices] IGetAllInvoicesUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromServices] IGetByIdInvoiceUseCase useCase, [FromRoute] long id)
    {
        var result = await useCase.Execute(id);

        return Ok(result);
    }
}
