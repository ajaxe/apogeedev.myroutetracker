using Microsoft.AspNetCore.Mvc;

namespace MyRouteTracker.Web.Controllers;

public abstract class ViewControllerBase : Controller
{
    protected IActionResult RedirectHome()
    {
        return RedirectToAction("Index", "Home");
    }
}