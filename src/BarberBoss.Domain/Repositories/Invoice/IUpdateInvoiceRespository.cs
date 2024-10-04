namespace BarberBoss.Domain.Repositories.Invoice;

public interface IUpdateInvoiceRespository
{
    Task<Entities.Invoice?> GetById(long id);
    void Update(Entities.Invoice invoice);
}
