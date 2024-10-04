namespace BarberBoss.Application.UseCases.Invoice.Delete;

public interface IDeleteInvoiceUseCase
{
    Task Execute(long id);
}
