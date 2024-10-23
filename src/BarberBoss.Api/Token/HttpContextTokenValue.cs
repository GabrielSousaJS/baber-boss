using BarberBoss.Domain.Security.Tokens;

namespace BarberBoss.Api.Token;

public class HttpContextTokenValue(IHttpContextAccessor contextAccessor) : ITokenProvider
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public string TokenOnRequest()
    {
        var authorization = _contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        return authorization["Bearer ".Length..].Trim();
    }
}
