using BarberBoss.Domain.Extensions;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Invoice;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BarberBoss.Application.UseCases.Invoice.Report.Excel;

public class GenerateInvoiceReportExcelUseCase(IInvoiceReadOnlyRepository repository) : IGenerateInvoiceReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IInvoiceReadOnlyRepository _repository = repository;

    public async Task<byte[]> Execute(DateOnly month)
    {
        var invoices = await _repository.FilterByMonth(month);

        if (invoices.Count == 0)
            return [];

        using var workbook = new XLWorkbook();

        workbook.Author = "Barber Boss";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Arial";

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var row = 2;

        foreach(var invoice in invoices)
        {
            worksheet.Cell($"A{row}").Value = invoice.Title;

            worksheet.Cell($"B{row}").Value = invoice.Date;
            worksheet.Cell($"B{row}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            
            worksheet.Cell($"C{row}").Value = invoice.PaymentType.PaymentTypeToString();
            worksheet.Cell($"C{row}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            worksheet.Cell($"D{row}").Value = invoice.Amount;
            worksheet.Cell($"D{row}").Style.NumberFormat.Format = $"{CURRENCY_SYMBOL} #,##0.00";

            worksheet.Cell($"E{row}").Value = invoice.Description;
            worksheet.Cell($"E{row}").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            row++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();

        workbook.SaveAs(file);

        return file.ToArray();
    }

    private static void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cells("A1").Value = ResourceReportMessages.TITLE;
        worksheet.Cells("B1").Value = ResourceReportMessages.DATE;
        worksheet.Cells("C1").Value = ResourceReportMessages.PAYMENT_TYPE;
        worksheet.Cells("D1").Value = ResourceReportMessages.AMOUNT;
        worksheet.Cells("E1").Value = ResourceReportMessages.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;

        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#205858");
        worksheet.Cells("A1:E1").Style.Font.FontColor = XLColor.FromHtml("#FFFFFF");

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
