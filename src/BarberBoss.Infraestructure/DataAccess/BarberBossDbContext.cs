using BarberBoss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infraestructure.DataAccess;

internal class BarberBossDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Invoice> Invoice { get; set; }
}
