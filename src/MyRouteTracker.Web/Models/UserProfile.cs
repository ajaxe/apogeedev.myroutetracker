using System.Security.Claims;

namespace MyRouteTracker.Web.Models;
public class UserProfile
{

    public ClaimsPrincipal User { get; }
    public UserProfile(ClaimsPrincipal user)
    {
        this.User = user;
    }
    private string _profileImageUrl = default!;
    public string ProfileImageUrl => _profileImageUrl
        ?? (_profileImageUrl = GetClaimValue("picture"));
    private string _name = default!;
    public string Name => _name ?? (_name = GetClaimValue("name"));

    private string _email = default!;
    public string Email => _email ?? (_email = GetClaimValue(AppClaimTypes.Email));

    private string _identifier = default!;
    public string Identifier => _identifier ?? (_identifier = GetClaimValue(AppClaimTypes.NameIdentifier));
    private string _idProvider = default!;
    public string IdProvider => _idProvider ?? (_idProvider = GetClaimValue(AppClaimTypes.IdP));
    public string UserIdentifier => $"{IdProvider}:{Identifier}";

    private string GetClaimValue(string claimType)
    {
        return User.Claims.FirstOrDefault(c => c.Type == claimType)?.Value
            ?? string.Empty;
    }
}