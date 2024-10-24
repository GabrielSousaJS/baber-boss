using AutoMapper;
using BarberBoss.Communication.Invoice.Responses;
using BarberBoss.Domain.Repositories.Invoice;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Invoice.GetById;

public class GetByIdInvoiceUseCase(IMapper mapper, IInvoiceReadOnlyRepository repository, ILoggedUser loggedUser) : IGetByIdInvoiceUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IInvoiceReadOnlyRepository _repository = repository;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<ResponseInvoiceJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetById(id, loggedUser.Id);

        if (result is null)
            throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);

        return _mapper.Map<ResponseInvoiceJson>(result);
    }
}
