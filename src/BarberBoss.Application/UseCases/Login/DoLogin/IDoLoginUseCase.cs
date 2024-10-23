using BarberBoss.Communication.Login.Request;
using BarberBoss.Communication.User.Responses;

namespace BarberBoss.Application.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestUserLoginJson request);
}
