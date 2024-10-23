using BarberBoss.Communication.User.Responses;

namespace BarberBoss.Application.UseCases.User.Profile;

public interface IUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
