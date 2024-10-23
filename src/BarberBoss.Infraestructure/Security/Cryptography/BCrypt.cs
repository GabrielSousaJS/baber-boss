using BarberBoss.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace BarberBoss.Infraestructure.Security.Cryptography;

public class BCrypt : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        var passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string password, string passwordHash) => BC.Verify(password, passwordHash);
}
