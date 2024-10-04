using BarberBoss.Communication.Invoice.Requests;
using BarberBoss.Communication.Invoice.Responses;

namespace BarberBoss.Application.UseCases.Invoice.Register;
public interface IRegisterInvoiceUseCase
{
    Task<ResponseRegisterInvoiceJson> Execute(RequestRegisterInvoiceJson request);
}
