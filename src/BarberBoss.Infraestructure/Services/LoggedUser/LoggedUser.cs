using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Infraestructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BarberBoss.Infraestructure.Services.LoggedUser;

internal class LoggedUser(BarberBossDbContext dbContext, ITokenProvider tokenProvider) : ILoggedUser
{
    private readonly BarberBossDbContext _dbContext = dbContext;
    private readonly ITokenProvider _tokenProvider = tokenProvider;   

    public async Task<User> Get()
    {
        var token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.UserIdentifier == Guid.Parse(identifier));
    }
}
