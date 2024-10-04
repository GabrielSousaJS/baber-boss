using AutoMapper;
using BarberBoss.Communication.Invoice.Responses;
using BarberBoss.Domain.Repositories.Invoice;

namespace BarberBoss.Application.UseCases.Invoice.GetAll;

public class GetAllInvoicesUseCase(IMapper mapper, IInvoiceReadOnlyRepository repository) : IGetAllInvoicesUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IInvoiceReadOnlyRepository _repository = repository;

    public async Task<ResponseInvoicesJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseInvoicesJson(_mapper.Map<List<ResponseShortInvoiceJson>>(result));
    }
}
