namespace BarberBoss.Application.UseCases.Invoice.Report.Excel;

public interface IGenerateInvoiceReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
