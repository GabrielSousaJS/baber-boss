using AutoMapper;
using BarberBoss.Communication.Invoice.Requests;
using BarberBoss.Communication.Invoice.Responses;
using BarberBoss.Communication.User.Requests;
using BarberBoss.Communication.User.Responses;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        // Invoice
        CreateMap<RequestInvoiceJson, Invoice>();
        
        // User
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());
        CreateMap<RequestUpdateUserJson, User>();
    }

    private void EntityToResponse()
    {
        // Invoice
        CreateMap<Invoice, ResponseRegisterInvoiceJson>();
        CreateMap<Invoice, ResponseShortInvoiceJson>();
        CreateMap<Invoice, ResponseInvoiceJson>();

        // User
        CreateMap<User, ResponseUserProfileJson>();
    }
}
