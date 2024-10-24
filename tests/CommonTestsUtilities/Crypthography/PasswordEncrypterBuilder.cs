using BarberBoss.Domain.Security.Cryptography;
using Moq;

namespace CommonTestsUtilities.Crypthography;

public class PasswordEncrypterBuilder
{
    private readonly Mock<IPasswordEncrypter> _mock;

    public PasswordEncrypterBuilder()
    {
        _mock = new Mock<IPasswordEncrypter>();

        _mock.Setup(encrypter => encrypter.Encrypt(It.IsAny<string>())).Returns("ir@*347829fj");
    }

    public PasswordEncrypterBuilder Verify(string? password = null)
    {
        if (!string.IsNullOrWhiteSpace(password))
            _mock.Setup(encrypter => encrypter.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        return this;
    }

    public IPasswordEncrypter Build() => _mock.Object;
}
