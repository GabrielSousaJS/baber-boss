using AutoMapper;
using BarberBoss.Communication.Invoice.Responses;
using BarberBoss.Domain.Repositories.Invoice;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Invoice.GetById;

public class GetByIdInvoiceUseCase(IMapper mapper, IInvoiceReadOnlyRepository repository) : IGetByIdInvoiceUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IInvoiceReadOnlyRepository _repository = repository;

    public async Task<ResponseInvoiceJson> Execute(long id)
    {
        var result = await _repository.GetById(id);

        if (result is null)
            throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);

        return _mapper.Map<ResponseInvoiceJson>(result);
    }
}
