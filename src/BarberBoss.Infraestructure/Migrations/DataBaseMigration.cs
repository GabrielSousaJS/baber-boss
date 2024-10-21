using BarberBoss.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infraestructure.Migrations;

public static class DataBaseMigration
{
    public async static Task MigrateDataBase(IServiceProvider services)
    {
        var dbContext = services.GetRequiredService<BarberBossDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
