using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Invoice;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infraestructure.DataAccess.Repositories;

internal class InvoiceRepository(BarberBossDbContext dbContext) : IInvoiceWriteOnlyRepository, IInvoiceReadOnlyRepository
{
    private readonly BarberBossDbContext _dbContext = dbContext;

    public async Task Add(Invoice invoice)
    {
        await _dbContext.AddAsync(invoice);
    }

    public async Task<List<Invoice>> GetAll()
    {
        return await _dbContext.Invoice.AsNoTracking().ToListAsync();
    }
}
