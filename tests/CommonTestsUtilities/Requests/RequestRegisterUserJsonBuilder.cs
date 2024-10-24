using BarberBoss.Communication.User.Requests;
using Bogus;

namespace CommonTestsUtilities.Requests;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(request => request.Name, faker => faker.Person.FullName)
            .RuleFor(request => request.Email, (faker, request) => faker.Internet.Email(request.Email))
            .RuleFor(request => request.BirthDate, _ => DateTime.Now.AddYears(-20))
            .RuleFor(request => request.Password, faker => faker.Internet.Password(prefix: "!Aa1"));
    }
}
