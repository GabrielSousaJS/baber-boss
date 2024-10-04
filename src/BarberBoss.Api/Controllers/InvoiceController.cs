using BarberBoss.Application.UseCases.Invoice.Delete;
using BarberBoss.Application.UseCases.Invoice.GetAll;
using BarberBoss.Application.UseCases.Invoice.GetById;
using BarberBoss.Application.UseCases.Invoice.Register;
using BarberBoss.Application.UseCases.Invoice.Update;
using BarberBoss.Communication.Errors.Response;
using BarberBoss.Communication.Invoice.Requests;
using BarberBoss.Communication.Invoice.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterInvoiceJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterInvoiceUseCase useCase,
        [FromBody] RequestInvoiceJson request)
    {
        var result = await useCase.Execute(request);

        return Created(string.Empty, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseInvoicesJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IGetAllInvoicesUseCase useCase)
    {
        var result = await useCase.Execute();

        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseInvoiceJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetByIdInvoiceUseCase useCase, [FromRoute] long id)
    {
        var result = await useCase.Execute(id);

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateInvoiceUseCase useCase,
        [FromRoute] long id,
        [FromBody] RequestInvoiceJson request
        )
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteInvoiceUseCase useCase,
        [FromRoute] long id
        )
    {
        await useCase.Execute(id);

        return NoContent();
    }
}
