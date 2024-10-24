using AutoMapper;
using BarberBoss.Communication.Invoice.Requests;
using BarberBoss.Communication.Invoice.Responses;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Invoice;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Invoice.Register;

public class RegisterInvoiceUseCase(
    IMapper mapper,
    IInvoiceWriteOnlyRepository repository,
    IUnitOfWork unitOfWork,
    ILoggedUser loggedUser) : IRegisterInvoiceUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IInvoiceWriteOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<ResponseRegisterInvoiceJson> Execute(RequestInvoiceJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var entity = _mapper.Map<Domain.Entities.Invoice>(request);
        entity.UserId = loggedUser.Id;

        await _repository.Add(entity);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisterInvoiceJson>(entity);
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
