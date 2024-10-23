using BarberBoss.Communication.Login.Request;
using BarberBoss.Communication.User.Responses;
using BarberBoss.Domain.Repositories.User;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase(
    IUserReadOnlyRepository repository,
    IPasswordEncrypter passwordEncrypter,
    IAccessTokenGenerator tokenGenerator) : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository _repository = repository;
    private readonly IPasswordEncrypter _passwordEncryter = passwordEncrypter;
    private readonly IAccessTokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<ResponseRegisterUserJson> Execute(RequestUserLoginJson request)
    {
        var user = await _repository.GetUserByEmail(request.Email) ?? throw new InvalidLoginException();

        var passwordMatch = _passwordEncryter.Verify(request.Password, user.Password);

        if (!passwordMatch)
            throw new InvalidLoginException();

        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = _tokenGenerator.GenerateToken(user)
        };
    }
}
