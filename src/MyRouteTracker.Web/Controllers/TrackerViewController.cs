using Microsoft.AspNetCore.Mvc;

namespace MyRouteTracker.Web.Controllers;

[Route("Tracker/Instance/{trackerId}")]
public class TrackerViewController : Controller
{
    public IActionResult Details(string trackerId)
    {
        ViewData["trackerId"] = trackerId;
        return View();
    }
}