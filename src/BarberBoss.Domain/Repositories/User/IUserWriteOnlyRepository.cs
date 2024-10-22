using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
    Task Add(Domain.Entities.User user);
}
