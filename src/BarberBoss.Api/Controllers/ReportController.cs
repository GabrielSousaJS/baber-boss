using BarberBoss.Application.UseCases.Invoice.Report.Excel;
using BarberBoss.Application.UseCases.Invoice.Reports.Pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Net.Mime;

namespace BarberBoss.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateInvoiceReportExcelUseCase useCase,
        [FromHeader] string month)
    {
        DateOnly.TryParseExact(month, "yyyy/MM", out DateOnly date);

        byte[] file = await useCase.Execute(date);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");

        return NoContent();
    }

    [HttpGet("pdf")]
    public async Task<IActionResult> GetPdf(
        [FromServices] IGenerateInvoiceReportPdfUseCase useCase,
        [FromHeader] string month)
    {
        DateOnly.TryParseExact(month, "yyyy/MM", out DateOnly date);

        byte[] file = await useCase.Execute(date);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "report.pdf");

        return NoContent();
    }
}
