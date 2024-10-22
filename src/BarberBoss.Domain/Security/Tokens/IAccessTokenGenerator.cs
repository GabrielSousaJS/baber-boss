using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string GenerateToken(User user);
}
