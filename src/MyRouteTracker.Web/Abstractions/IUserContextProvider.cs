using MyRouteTracker.Web.Models;

namespace MyRouteTracker.Web.Abstractions;
public interface IUserContextProvider
{
    Task<UserProfile?> GetUserProfile();
}