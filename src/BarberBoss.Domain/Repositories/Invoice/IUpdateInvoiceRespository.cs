namespace BarberBoss.Domain.Repositories.Invoice;

public interface IUpdateInvoiceRespository
{
    Task<Entities.Invoice?> GetById(long id, long userId);
    void Update(Entities.Invoice invoice);
}
