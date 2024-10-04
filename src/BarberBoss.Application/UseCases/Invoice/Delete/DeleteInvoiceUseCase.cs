using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Invoice;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Invoice.Delete;

public class DeleteInvoiceUseCase(IInvoiceWriteOnlyRepository repository, IUnitOfWork unitOfWork) : IDeleteInvoiceUseCase
{
    private readonly IInvoiceWriteOnlyRepository _repository = repository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(long id)
    {
        var result = await _repository.Delete(id);

        if (!result)
            throw new NotFoundException(ResourceErrorMessages.BILLING_NOT_FOUND);

        await _unitOfWork.Commit();
    }
}
