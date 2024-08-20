using Htmx;
using Microsoft.AspNetCore.Mvc;
using MyRouteTracker.Web.Abstractions.Services;
using MyRouteTracker.Web.Models;

namespace MyRouteTracker.Web.Controllers;

public class TrackerViewController : ViewControllerBase
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
    public async Task<IActionResult> Start(string? trackerId)
    {
        if (!Request.IsHtmx())
        {
            return RedirectHome();
        }

        RouteDataSet? tracker = null;

        if (string.IsNullOrWhiteSpace(trackerId))
            tracker = await dataService.CreateNewRoute();
        else
            tracker = await dataService.GetRoute(trackerId);

        TriggerReloadRoutes();

        return PartialView("_TrackerCollector", (RouteDataSetViewModel)tracker!);
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string trackerId)
    {
        if (!Request.IsHtmx())
        {
            return RedirectHome();
        }

        await dataService.DeleteRoute(trackerId);

        TriggerReloadRoutes();

        return NoContent();
    }
}