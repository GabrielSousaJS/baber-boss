using AutoMapper;
using BarberBoss.Communication.Invoice.Requests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Invoice;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Invoice.Update;

public class UpdateInvoiceUseCase(IMapper mapper, IUpdateInvoiceRespository repository, IUnitOfWork unitOfWork) : IUpdateInvoiceUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IUpdateInvoiceRespository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(long id, RequestInvoiceJson request)
    {
        Validate(request);

        var invoice = await _repository.GetById(id) ?? throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);
        
        _mapper.Map(request, invoice);

        _repository.Update(invoice);

        await _unitOfWork.Commit();
    }

    private static void Validate(RequestInvoiceJson request)
    {
        var validator = new InvoiceValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(failure => failure.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
