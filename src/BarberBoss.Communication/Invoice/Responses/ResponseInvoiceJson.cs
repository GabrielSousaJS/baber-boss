using BarberBoss.Communication.Enums;

namespace BarberBoss.Communication.Invoice.Responses;

public record ResponseInvoiceJson(
    long Id,
    string Title,
    string Description,
    DateTime Date,
    decimal Amount,
    PaymentType PaymentType
);