using AutoMapper;
using BarberBoss.Communication.User.Responses;
using BarberBoss.Domain.Services.LoggedUser;

namespace BarberBoss.Application.UseCases.User.Profile;

public class UserProfileUseCase(
    ILoggedUser loggedUser,
    IMapper mapper) : IUserProfileUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IMapper _mapper = mapper;

    public async Task<ResponseUserProfileJson> Execute()
    {
        var user = await _loggedUser.Get();

        return _mapper.Map<ResponseUserProfileJson>(user);
    }
}
