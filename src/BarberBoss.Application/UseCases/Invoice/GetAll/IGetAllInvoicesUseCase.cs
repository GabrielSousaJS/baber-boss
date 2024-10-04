using BarberBoss.Communication.Invoice.Responses;

namespace BarberBoss.Application.UseCases.Invoice.GetAll;

public interface IGetAllInvoicesUseCase
{
    Task<ResponseInvoicesJson> Execute();
}
