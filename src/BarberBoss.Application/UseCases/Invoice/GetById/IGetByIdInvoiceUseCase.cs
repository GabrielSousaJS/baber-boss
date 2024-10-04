using BarberBoss.Communication.Invoice.Responses;

namespace BarberBoss.Application.UseCases.Invoice.GetById;

public interface IGetByIdInvoiceUseCase
{
    Task<ResponseInvoiceJson> Execute(long id);
}
