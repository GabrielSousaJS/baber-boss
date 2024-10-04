using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Invoice.Requests;

public class RequestRegisterInvoiceJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
}
