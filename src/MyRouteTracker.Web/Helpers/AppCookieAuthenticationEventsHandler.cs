using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MyRouteTracker.Web.Helpers;

public class AppCookieAuthenticationEventsHandler : CookieAuthenticationEvents
{
    public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> ctx)
    {
        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
        {
            ctx.Response.StatusCode = 401;
        }

        return Task.CompletedTask;
    }
    public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> ctx)
    {
        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
        {
            ctx.Response.StatusCode = 403;
        }

        return Task.CompletedTask;
    }
}