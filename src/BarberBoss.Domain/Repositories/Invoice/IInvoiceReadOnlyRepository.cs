namespace BarberBoss.Domain.Repositories.Invoice;

public interface IInvoiceReadOnlyRepository
{
    Task<List<Entities.Invoice>> GetAll();
}
