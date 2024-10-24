using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Invoice;
public interface IInvoiceWriteOnlyRepository
{
    Task Add(Entities.Invoice invoice);
    Task<bool> Delete(long id, long userId);
}
