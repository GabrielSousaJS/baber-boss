using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infraestructure.DataAccess.Repositories;

internal class UserRepository(BarberBossDbContext dbContext) : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly BarberBossDbContext _dbContext = dbContext;

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> ExistsActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));
    }
}
