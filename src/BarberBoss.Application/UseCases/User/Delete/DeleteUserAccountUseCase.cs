using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.User;
using BarberBoss.Domain.Services.LoggedUser;

namespace BarberBoss.Application.UseCases.User.Delete;

public class DeleteUserAccountUseCase(
    IUserWriteOnlyRepository repository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork) : IDeleteUserAccountUseCase
{
    private readonly IUserWriteOnlyRepository _repository = repository;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute()
    {
        var loggedUser = await _loggedUser.Get();

        await _repository.Delete(loggedUser);

        await _unitOfWork.Commit();
    }
}
