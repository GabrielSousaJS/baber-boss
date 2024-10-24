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

    public async Task<List<Invoice>> GetAll(long userId)
    {
        return await _dbContext.Invoice.AsNoTracking().Where(invoice => invoice.UserId == userId).ToListAsync();
    }

    public async Task<Invoice?> GetById(long id, long userId)
    {
        return await _dbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(invoice => invoice.Id == id && invoice.UserId == userId);
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

    async Task<Invoice?> IUpdateInvoiceRespository.GetById(long id, long userId)
    {
        return await _dbContext.Invoice.FirstOrDefaultAsync(invoice => invoice.Id == id && invoice.UserId == userId);
    }

    public void Update(Invoice invoice)
    {
        _dbContext.Invoice.Update(invoice);
    }

    public async Task<bool> Delete(long id, long userId)
    {
        var invoice = await _dbContext.Invoice.FirstOrDefaultAsync(invoice => invoice.Id == id && invoice.UserId == userId);

        if (invoice is null)
            return false;

        _dbContext.Invoice.Remove(invoice);

        return true;
    }
}
