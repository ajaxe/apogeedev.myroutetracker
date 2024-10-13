using Htmx;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyRouteTracker.Web.Controllers;

[Authorize]
public abstract class ViewControllerBase : Controller
{
    protected IActionResult RedirectHome()
    {
        return RedirectToAction("Index", "Home");
    }
    protected void TriggerReloadRoutes(HtmxTriggerTiming timing = HtmxTriggerTiming.Default)
    {
        Response.Htmx(h =>
        {
            h.WithTrigger("reloadRoutes", timing);
        });
    }
}