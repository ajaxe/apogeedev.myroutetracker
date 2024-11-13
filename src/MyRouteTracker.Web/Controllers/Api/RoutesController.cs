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
}