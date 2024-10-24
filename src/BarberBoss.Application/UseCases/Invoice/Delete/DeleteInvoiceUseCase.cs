using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Invoice;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Invoice.Delete;

public class DeleteInvoiceUseCase(IInvoiceWriteOnlyRepository repository, IUnitOfWork unitOfWork, ILoggedUser loggedUser) : IDeleteInvoiceUseCase
{
    private readonly IInvoiceWriteOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.Delete(id, loggedUser.Id);

        if (!result)
            throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);

        await _unitOfWork.Commit();
    }
}
