namespace BarberBoss.Application.UseCases.Invoice.Reports.Pdf;

public interface IGenerateInvoiceReportPdfUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
