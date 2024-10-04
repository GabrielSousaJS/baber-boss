using AutoMapper;
using BarberBoss.Communication.Invoice.Requests;
using BarberBoss.Communication.Invoice.Responses;
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
        CreateMap<RequestRegisterInvoiceJson, Invoice>();
    }

    private void EntityToResponse()
    {
        CreateMap<Invoice, ResponseRegisterInvoiceJson>();
    }
}