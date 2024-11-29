using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRouteTracker.Web.Abstractions.Services;
using MyRouteTracker.Web.Models;

namespace MyRouteTracker.Web.Controllers.Api;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class RoutesController : ControllerBase
{
    private readonly IRouteDataService dataService;

    public RoutesController(IRouteDataService dataService)
    {
        this.dataService = dataService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoutes(int? tzOffset)
    {
        var data = await dataService.GetRoutes();
        var vm = new RouteDataSetListViewModel
        {
            Routes = data.Select(r => (RouteDataSetViewModel)r).ToList(),
        };

        return Ok(vm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewRoute(string? trackerId)
    {
        RouteDataSet? tracker = null;

        if (string.IsNullOrWhiteSpace(trackerId))
            tracker = await dataService.CreateNewRoute();
        else
            tracker = await dataService.GetRoute(trackerId);

        return Ok(tracker);
    }
    [HttpDelete("{trackerId}")]
    public async Task<IActionResult> DeleteRoute(string trackerId)
    {
        if (!string.IsNullOrWhiteSpace(trackerId))
            await dataService.DeleteRoute(trackerId);

        return NoContent();
    }
}