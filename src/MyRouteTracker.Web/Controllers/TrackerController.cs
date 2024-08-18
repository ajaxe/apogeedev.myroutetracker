using Microsoft.AspNetCore.Mvc;
using MyRouteTracker.Web.Abstractions.Services;
using MyRouteTracker.Web.Models;

namespace MyRouteTracker.Web.Controllers;
public class TrackerController : Controller
{
    private readonly IRouteDataService dataService;

    public TrackerController(IRouteDataService dataService)
    {
        this.dataService = dataService;
    }

    public async Task<IActionResult> List()
    {
        var data = await dataService.GetRoutes();
        var vm = new RouteDataSetListViewModel
        {
            Routes = data.Select(r => (RouteDataSetViewModel)r).ToList(),
        };
        return View(vm);
    }
}