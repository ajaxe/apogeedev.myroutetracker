using Htmx;
using Microsoft.AspNetCore.Mvc;
using MyRouteTracker.Web.Abstractions.Services;
using MyRouteTracker.Web.Models;

namespace MyRouteTracker.Web.Controllers;
public class TrackerController : ViewControllerBase
{
    private readonly IRouteDataService dataService;

    public TrackerController(IRouteDataService dataService)
    {
        this.dataService = dataService;
    }

    public IActionResult List()
    {
        return View();
    }
    public async Task<IActionResult> ListRoutes()
    {
        if (!Request.IsHtmx())
        {
            return RedirectHome();
        }

        var data = await dataService.GetRoutes();
        var vm = new RouteDataSetListViewModel
        {
            Routes = data.Select(r => (RouteDataSetViewModel)r).ToList(),
        };

        return PartialView("_ListRoutes", vm);
    }
}