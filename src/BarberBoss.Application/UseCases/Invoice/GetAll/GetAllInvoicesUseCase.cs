using AutoMapper;
using BarberBoss.Communication.Invoice.Responses;
using BarberBoss.Domain.Repositories.Invoice;
using BarberBoss.Domain.Services.LoggedUser;

namespace BarberBoss.Application.UseCases.Invoice.GetAll;

public class GetAllInvoicesUseCase(IMapper mapper, IInvoiceReadOnlyRepository repository, ILoggedUser loggedUser) : IGetAllInvoicesUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IInvoiceReadOnlyRepository _repository = repository;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<ResponseInvoicesJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetAll(loggedUser.Id);

        return new ResponseInvoicesJson(_mapper.Map<List<ResponseShortInvoiceJson>>(result));
    }
}
