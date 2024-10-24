namespace BarberBoss.Domain.Repositories.Invoice;

public interface IInvoiceReadOnlyRepository
{
    Task<List<Entities.Invoice>> GetAll(long userId);
    Task<Entities.Invoice?> GetById(long id, long userId);
    Task<List<Entities.Invoice>> FilterByMonth(DateOnly date);
}
