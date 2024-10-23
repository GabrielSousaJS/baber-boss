using BarberBoss.Communication.User.Requests;

namespace BarberBoss.Application.UseCases.User.Update;

public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}
