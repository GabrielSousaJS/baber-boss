using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using Bogus;
using CommonTestsUtilities.Crypthography;

namespace CommonTestsUtilities.Entities;

public class UserBuilder
{
    public static User Build(string roles = Roles.TEAM_MEMBER)
    {
        var encrypter = new PasswordEncrypterBuilder().Build();

        var user = new Faker<User>()
            .RuleFor(user => user.Name, faker => faker.Person.FullName)
            .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Name))
            .RuleFor(user => user.BirthDate, _ => DateTime.Now.AddYears(-18))
            .RuleFor(user => user.Password, (_, user) => encrypter.Encrypt(user.Password))
            .RuleFor(user => user.UserIdentifier, _ => Guid.NewGuid())
            .RuleFor(user => user.Role, _ => roles);

        return user;
    }
}
