using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Invoice;
using BarberBoss.Infraestructure.DataAccess;
using BarberBoss.Infraestructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infraestructure;

public static class DependencyInjectionExtension
{
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));

        services.AddDbContext<BarberBossDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        // Unidade de trabalho
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Invoice
        services.AddScoped<IInvoiceWriteOnlyRepository, InvoiceRepository>();
        services.AddScoped<IInvoiceReadOnlyRepository, InvoiceRepository>();
        services.AddScoped<IUpdateInvoiceRespository, InvoiceRepository>();
    }
}
