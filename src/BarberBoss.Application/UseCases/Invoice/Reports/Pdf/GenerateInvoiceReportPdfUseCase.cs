using BarberBoss.Application.UseCases.Invoice.Reports.Pdf.Colors;
using BarberBoss.Domain.Extensions;
using BarberBoss.Domain.Reports;
using BarberBoss.Domain.Repositories.Invoice;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Reflection;

namespace BarberBoss.Application.UseCases.Invoice.Reports.Pdf;

public class GenerateInvoiceReportPdfUseCase(IInvoiceReadOnlyRepository repository) : IGenerateInvoiceReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_ROW_INVOICE_TABLE = 25;
    private readonly IInvoiceReadOnlyRepository _repository = repository;

    public async Task<byte[]> Execute(DateOnly month)
    {
        var invoices = await _repository.FilterByMonth(month);

        if (invoices.Count == 0)
            return [];

        var document = CreateDocument(month);
        var page = CreatePage(document);

        CreateHeaderWithProfilePhotoAndName(page);

        var totalBilling = invoices.Sum(invoice => invoice.Amount);

        CreateTotalSpentSection(page, month, totalBilling);

        foreach (var invoice in invoices)
        {
            var table = CreateInvoiceTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_INVOICE_TABLE;

            AddInvoiceTitle(row.Cells[0], invoice.Title);
            AddHeaderForAmount(row.Cells[3]);

            row = table.AddRow();
            row.Height = HEIGHT_ROW_INVOICE_TABLE;

            row.Cells[0].AddParagraph(invoice.Date.ToString("d"));
            SetStyleBaseForInvoiceInformation(row.Cells[0]);

            row.Cells[1].AddParagraph(invoice.Date.ToString("t"));
            SetStyleBaseForInvoiceInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(invoice.PaymentType.PaymentTypeToString());
            SetStyleBaseForInvoiceInformation(row.Cells[2]);

            AddAmountForBilling(row.Cells[3], invoice.Amount);

            if (string.IsNullOrWhiteSpace(invoice.Description) == false)
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW_INVOICE_TABLE;

                descriptionRow.Cells[0].AddParagraph(invoice.Description);
                descriptionRow.Cells[0].Format.Font = new Font { Size = 10, Color = ColorsHelper.GRAY };
                descriptionRow.Cells[0].Format.LeftIndent = 20;
                descriptionRow.Cells[0].Shading.Color = Colors.ColorsHelper.WHITE_DARK;
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;

                row.Cells[3].MergeDown = 1;
            }

            AddWhiteSpace(table);
        }

        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportMessages.BILLING} {month.ToString("Y")}";
        document.Info.Author = "Barber Boss";

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;

        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 40;
        section.PageSetup.BottomMargin = 40;

        return section;
    }

    private void CreateHeaderWithProfilePhotoAndName(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("80");
        table.AddColumn("280");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryname = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directoryname!, "Logo", "logo.png");

        row.Cells[0].AddImage(pathFile);

        row.Cells[1].AddParagraph("BARBEARIA DO JOÃO");
        row.Cells[1].Format.Font = new Font { Size = 16, Bold = true };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }

    private void CreateTotalSpentSection(Section page, DateOnly month, decimal totalBilling)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = ResourceReportMessages.TOTAL_BILLED;

        paragraph.AddFormattedText(title, new Font { Size = 16, Bold = true });

        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{CURRENCY_SYMBOL} {totalBilling}", new Font { Size = 50 });
    }

    private Table CreateInvoiceTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    private void AddInvoiceTitle(Cell cell, string invoiceTitle)
    {
        cell.AddParagraph(invoiceTitle);
        cell.Format.Font = new Font { Size = 14, Color = ColorsHelper.WHITE };
        cell.Format.LeftIndent = 20;

        cell.Shading.Color = Colors.ColorsHelper.BLUE_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
    }

    private void AddHeaderForAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportMessages.AMOUNT);
        cell.Format.Font = new Font { Size = 14, Color = ColorsHelper.WHITE };

        cell.Shading.Color = Colors.ColorsHelper.BLUE_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void SetStyleBaseForInvoiceInformation(Cell cell)
    {
        cell.Format.Font = new Font { Size = 12, Color = ColorsHelper.BLACK };

        cell.Shading.Color = ColorsHelper.BLUE_HIGH_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.Format.LeftIndent = 20;
    }

    private void AddAmountForBilling(Cell cell, decimal amount)
    {
        cell.AddParagraph($"{CURRENCY_SYMBOL} {amount}");
        cell.Format.Font = new Font { Size = 14, Color = ColorsHelper.BLACK };

        cell.Shading.Color = ColorsHelper.WHITE;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = HEIGHT_ROW_INVOICE_TABLE;
        row.Borders.Visible = false;
    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}