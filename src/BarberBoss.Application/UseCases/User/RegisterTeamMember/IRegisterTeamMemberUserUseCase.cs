using BarberBoss.Communication.User.Requests;
using BarberBoss.Communication.User.Responses;

namespace BarberBoss.Application.UseCases.User.Register;

public interface IRegisterTeamMemberUserUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
}
