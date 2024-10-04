using BarberBoss.Communication.Invoice.Requests;

namespace BarberBoss.Application.UseCases.Invoice.Update;

public interface IUpdateInvoiceUseCase
{
    Task Execute(long id, RequestInvoiceJson request);
}
