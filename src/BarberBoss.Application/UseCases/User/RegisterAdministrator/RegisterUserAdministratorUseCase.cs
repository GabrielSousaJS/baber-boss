using AutoMapper;
using BarberBoss.Communication.User.Requests;
using BarberBoss.Communication.User.Responses;
using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.User;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using FluentValidation.Results;

namespace BarberBoss.Application.UseCases.User.RegisterAdministrator;

public class RegisterUserAdministratorUseCase(
    IMapper mapper,
    IUserReadOnlyRepository readOnlyrepository,
    IUserWriteOnlyRepository writeOnlyRepository,
    IPasswordEncrypter passwordEncrypter,
    IUnitOfWork unitOfWork,
    IAccessTokenGenerator tokenGenerator) : IRegisterUserAdministratorUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserReadOnlyRepository _readOnlyRepository = readOnlyrepository;
    private readonly IUserWriteOnlyRepository _writeOnlyRepository = writeOnlyRepository;
    private readonly IPasswordEncrypter _passwordEncrypter = passwordEncrypter;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAccessTokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.UserIdentifier = Guid.NewGuid();
        user.Role = Roles.ADMIN;

        user.Password = _passwordEncrypter.Encrypt(request.Password);

        await _writeOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = _tokenGenerator.GenerateToken(user),
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExists = await _readOnlyRepository.ExistsActiveUserWithEmail(request.Email);

        if (emailExists)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}
