using BarberBoss.Application.AutoMapper;
using BarberBoss.Application.UseCases.Invoice.Delete;
using BarberBoss.Application.UseCases.Invoice.GetAll;
using BarberBoss.Application.UseCases.Invoice.GetById;
using BarberBoss.Application.UseCases.Invoice.Register;
using BarberBoss.Application.UseCases.Invoice.Report.Excel;
using BarberBoss.Application.UseCases.Invoice.Reports.Pdf;
using BarberBoss.Application.UseCases.Invoice.Update;
using BarberBoss.Application.UseCases.Login.DoLogin;
using BarberBoss.Application.UseCases.User.Profile;
using BarberBoss.Application.UseCases.User.Register;
using BarberBoss.Application.UseCases.User.RegisterAdministrator;
using BarberBoss.Domain.Services.LoggedUser;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddMapper(services);
        AddUseCases(services);
    }

    private static void AddMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        // Invoice
        services.AddScoped<IRegisterInvoiceUseCase, RegisterInvoiceUseCase>();
        services.AddScoped<IGetAllInvoicesUseCase, GetAllInvoicesUseCase>();
        services.AddScoped<IGetByIdInvoiceUseCase, GetByIdInvoiceUseCase>();
        services.AddScoped<IUpdateInvoiceUseCase, UpdateInvoiceUseCase>();
        services.AddScoped<IDeleteInvoiceUseCase, DeleteInvoiceUseCase>();

        // Invoice -> Report
        services.AddScoped<IGenerateInvoiceReportExcelUseCase, GenerateInvoiceReportExcelUseCase>();
        services.AddScoped<IGenerateInvoiceReportPdfUseCase, GenerateInvoiceReportPdfUseCase>();

        // User
        services.AddScoped<IRegisterUserAdministratorUseCase, RegisterUserAdministratorUseCase>();
        services.AddScoped<IRegisterTeamMemberUserUseCase, RegisterTeamMemberUserUseCase>();
        services.AddScoped<IUserProfileUseCase, UserProfileUseCase>();

        // Login
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
    }
}
