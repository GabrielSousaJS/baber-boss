using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.Invoice;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infraestructure.DataAccess.Repositories;

internal class InvoiceRepository(BarberBossDbContext dbContext) : IInvoiceWriteOnlyRepository, IInvoiceReadOnlyRepository, IUpdateInvoiceRespository
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

    public async Task<Invoice?> GetById(long id)
    {
        return await _dbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(invoice => invoice.Id == id);
    }

    public async Task<List<Invoice>> FilterByMonth(DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;
        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);

        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59).Date;

        return await _dbContext
            .Invoice
            .AsNoTracking()
            .Where(invoice => invoice.Date >= startDate && invoice.Date <= endDate)
            .OrderBy(invoice => invoice.Date)
            .ToListAsync();
    }

    async Task<Invoice?> IUpdateInvoiceRespository.GetById(long id)
    {
        return await _dbContext.Invoice.FirstOrDefaultAsync(invoice => invoice.Id == id);
    }

    public void Update(Invoice invoice)
    {
        _dbContext.Invoice.Update(invoice);
    }

    public async Task<bool> Delete(long id)
    {
        var invoice = await _dbContext.Invoice.FirstOrDefaultAsync(invoice => invoice.Id == id);

        if (invoice is null)
            return false;

        _dbContext.Invoice.Remove(invoice);

        return true;
    }
}
