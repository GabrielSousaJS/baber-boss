using BarberBoss.Communication.User.Requests;
using BarberBoss.Communication.User.Responses;

namespace BarberBoss.Application.UseCases.User.RegisterAdministrator;

public interface IRegisterUserAdministratorUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
}
