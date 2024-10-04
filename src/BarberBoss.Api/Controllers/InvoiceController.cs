using BarberBoss.Application.UseCases.Invoice.Register;
using BarberBoss.Communication.Invoice.Requests;
using Microsoft.AspNetCore.Http;
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
}
