namespace BarberBoss.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<Entities.User?> GetUserByEmail(string email);
    Task<bool> ExistsActiveUserWithEmail(string email);
}
