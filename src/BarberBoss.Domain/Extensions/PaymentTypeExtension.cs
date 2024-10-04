using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Reports;

namespace BarberBoss.Domain.Extensions;

public static class PaymentTypeExtension
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => ResourceReportMessages.CASH,
            PaymentType.CreditCard => ResourceReportMessages.CREDIT_CARD,
            PaymentType.DebitCard => ResourceReportMessages.DEBIT_CAD,
            PaymentType.EletronicTransfer => ResourceReportMessages.ELETRONIC_TRANSFER,
            PaymentType.Pix => ResourceReportMessages.PIX,
            _ => string.Empty,
        };
    }
}
