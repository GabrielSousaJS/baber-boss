namespace BarberBoss.Communication.Invoice.Responses;

public record ResponseShortInvoiceJson(
    long Id,
    string Title,
    decimal Amount
);
