using Microsoft.AspNetCore.Mvc;
using MyRouteTracker.Web.Abstractions.Services;
using MyRouteTracker.Web.Models;

namespace MyRouteTracker.Web.Controllers;

[Route("Tracker/Instance/{trackerId}")]
public class TrackerViewController : Controller
{
    private readonly IRouteDataService dataService;

    public TrackerViewController(IRouteDataService dataService)
    {
        this.dataService = dataService;
    }

    public async Task<IActionResult> Details(string trackerId)
    {
        var existing = await dataService.GetRoute(trackerId);
        RouteDataSetViewModel? vm = null;

        if (existing != null) vm = (RouteDataSetViewModel)existing;

        return View(vm);
    }
}