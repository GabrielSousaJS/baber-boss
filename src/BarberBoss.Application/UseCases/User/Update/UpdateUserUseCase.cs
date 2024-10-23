using AutoMapper;
using BarberBoss.Communication.User.Requests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.User;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;
using FluentValidation.Results;

namespace BarberBoss.Application.UseCases.User.Update;

public class UpdateUserUseCase(
    IMapper mapper,
    IUserReadOnlyRepository readOnlyRepository,
    IUserUpdateOnlyRepository updateRepository,
    ILoggedUser loggedUser,
    IUnitOfWork unitOfWork): IUpdateUserUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserReadOnlyRepository _readOnlyRepository = readOnlyRepository;
    private readonly IUserUpdateOnlyRepository _updateRepository = updateRepository;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Execute(RequestUpdateUserJson request)
    {
        var loggedUser = await _loggedUser.Get();

        await Validate(request, loggedUser.Email);

        var user = await _updateRepository.GetById(loggedUser.Id);

        _mapper.Map(request, user);

        _updateRepository.Update(user);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUserJson request, string currentEmail)
    {
        var result = new UpdateUserValidator().Validate(request);

        if (!request.Email.Equals(currentEmail))
        {
            var emailExists = await _readOnlyRepository.ExistsActiveUserWithEmail(request.Email);

            if (emailExists)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
            }
        }

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}
