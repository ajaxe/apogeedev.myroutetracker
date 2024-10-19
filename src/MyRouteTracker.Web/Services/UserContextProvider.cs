
using System.Security.Claims;
using MyRouteTracker.Web.Abstractions;
using MyRouteTracker.Web.Models;

namespace MyRouteTracker.Web.Services;
public class UserContextProvider : IUserContextProvider
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserContextProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }
    public Task<UserProfile?> GetUserProfile()
    {
        if (!(httpContextAccessor.HttpContext!.User.Identity?.IsAuthenticated ?? false))
        {
            return Task.FromResult(default(UserProfile?));
        }
        var profile = new UserProfile(httpContextAccessor.HttpContext!.User);
        return Task.FromResult<UserProfile?>(profile);
    }
}