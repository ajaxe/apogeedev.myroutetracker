namespace MyRouteTracker.Web.Models;

public class SessionViewModel
{
    public bool IsLoggedIn { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Identifier { get; set; }
    public string? IdProvider { get; set; }
    public string? UserIdentifier { get; set; }
    public string? LoginUrl { get; set; }
    public string? LogoutUrl { get; set; }

    public static explicit operator SessionViewModel(UserProfile profile)
    {
        return new SessionViewModel
        {
            IsLoggedIn = true,
            ProfileImageUrl = profile.ProfileImageUrl,
            Name = profile.Name,
            Email = profile.Email,
            Identifier = profile.Identifier,
            IdProvider = profile.IdProvider,
            UserIdentifier = profile.UserIdentifier,
        };
    }
}