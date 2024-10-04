namespace BarberBoss.Domain.Repositories.Invoice;

public interface IInvoiceReadOnlyRepository
{
    Task<List<Entities.Invoice>> GetAll();
    Task<Entities.Invoice?> GetById(long id);
    Task<List<Entities.Invoice>> FilterByMonth(DateOnly date);
}
