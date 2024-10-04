using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Invoice;

namespace BarberBoss.Infraestructure.DataAccess.Repositories;

internal class InvoiceRepository(BarberBossDbContext dbContext) : IInvoiceWriteOnlyRepository
{
    private readonly BarberBossDbContext _dbContext = dbContext;

    public async Task Add(Invoice invoice)
    {
        await _dbContext.AddAsync(invoice);
    }
}
