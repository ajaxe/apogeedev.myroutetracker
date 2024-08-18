using Microsoft.AspNetCore.Mvc;
using MyRouteTracker.Web.Abstractions.Services;

namespace MyRouteTracker.Web.Controllers.Api;

[Route("api/[controller]")]
public class IngestController : Controller
{
    public IngestController()
    {

    }

    [HttpPost("{userId}/{routeId}/datapoint")]
    public async Task<IActionResult> PostDataPoint(string userId, string routeId,
        [FromBody] RouteDataPointInput data,
        [FromServices] IDataIngestionService ingestionService)
    {
        await ingestionService.Ingest(userId, routeId, data);
        return Ok();
    }
}