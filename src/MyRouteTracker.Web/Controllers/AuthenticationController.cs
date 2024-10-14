using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

namespace MyRouteTracker.Web.Controllers;

public class AuthenticationController : Controller
{
    [HttpGet("~/login")]
    public ActionResult LogIn(string returnUrl)
    {
        var properties = new AuthenticationProperties
        {
            // Only allow local return URLs to prevent open redirect attacks.
            RedirectUri = Url.IsLocalUrl(returnUrl) ? returnUrl : "/"
        };

        // Ask the OpenIddict client middleware to redirect the user agent to the identity provider.
        return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
    }

    [HttpGet("~/logout", Name = "logout")]
    [HttpPost("~/logout", Name = "logout")]
    //[ValidateAntiForgeryToken]
    public async Task<ActionResult> LogOut(string returnUrl)
    {
        // Retrieve the identity stored in the local authentication cookie. If it's not available,
        // this indicate that the user is already logged out locally (or has not logged in yet).
        //
        // For scenarios where the default authentication handler configured in the ASP.NET Core
        // authentication options shouldn't be used, a specific scheme can be specified here.
        var result = await HttpContext.AuthenticateAsync();
        if (result is not { Succeeded: true })
        {
            // Only allow local return URLs to prevent open redirect attacks.
            return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
        }

        // Remove the local authentication cookie before triggering a redirection to the remote server.
        //
        // For scenarios where the default sign-out handler configured in the ASP.NET Core
        // authentication options shouldn't be used, a specific scheme can be specified here.
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var properties = new AuthenticationProperties(new Dictionary<string, string?>
        {
            // While not required, the specification encourages sending an id_token_hint
            // parameter containing an identity token returned by the server for this user.
            //[OpenIdConnectDefaults.Properties.IdentityTokenHint] =
            //    result.Properties.GetTokenValue(OpenIdConnectDefaults.Tokens.BackchannelIdentityToken)
        })
        {
            // Only allow local return URLs to prevent open redirect attacks.
            RedirectUri = Url.IsLocalUrl(returnUrl) ? returnUrl : "/"//$"{Startup.IdpBaseUrl.TrimEnd('/')}/connect/logout",
        };

        // Ask the OpenIddict client middleware to redirect the user agent to the identity provider.
        return SignOut(properties, OpenIdConnectDefaults.AuthenticationScheme);
    }

    [HttpGet("~/callback/login/local")]
    [IgnoreAntiforgeryToken]
    public async Task<ActionResult> LogInCallback()
    {
        // Retrieve the authorization data validated by OpenIddict as part of the callback handling.
        var result = await HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);

        if (result.Principal is not ClaimsPrincipal { Identity.IsAuthenticated: true })
        {
            throw new InvalidOperationException("The external authorization data cannot be used for authentication.");
        }

        // Build an identity based on the external claims and that will be used to create the authentication cookie.
        var identity = new ClaimsIdentity(
            authenticationType: "ExternalLogin",
            nameType: ClaimTypes.Name,
            roleType: ClaimTypes.Role);

        var properties = new AuthenticationProperties(result.Properties!.Items)
        {
            RedirectUri = result.Properties.RedirectUri ?? "/"
        };

        /*properties.StoreTokens(result.Properties.GetTokens().Where(token => token.Name is
            // Preserve the access, identity and refresh tokens returned in the token response, if available.
            OpenIdConnectDefaults.Tokens.BackchannelAccessToken or
            OpenIdConnectDefaults.Tokens.BackchannelIdentityToken or
            OpenIdConnectDefaults.Tokens.RefreshToken));
        */

        return SignIn(new ClaimsPrincipal(identity), properties);
    }

    [HttpGet("~/callback/logout/local")]
    [IgnoreAntiforgeryToken]
    public async Task<ActionResult> LogOutCallback()
    {
        // Retrieve the data stored by OpenIddict in the state token created when the logout was triggered.
        var result = await HttpContext.AuthenticateAsync(OpenIdConnectDefaults.AuthenticationScheme);

        // In this sample, the local authentication cookie is always removed before the user agent is redirected
        // to the authorization server. Applications that prefer delaying the removal of the local cookie can
        // remove the corresponding code from the logout action and remove the authentication cookie in this action.

        return Redirect(result!.Properties!.RedirectUri ?? "/");
    }
}