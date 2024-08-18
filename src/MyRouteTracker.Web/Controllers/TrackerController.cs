using Microsoft.AspNetCore.Mvc;

namespace MyRouteTracker.Web.Controllers;
public class TrackerController : Controller
{
    public IActionResult List()
    {
        return View();
    }
}