using Microsoft.AspNetCore.Mvc;
using MyRouteTracker.Web.Abstractions.Services;

namespace MyRouteTracker.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class IngestController : ControllerBase
{
    public IngestController()
    {

    }

    [HttpPost("{userId}/{routeId}/datapoint")]
    public async Task<IActionResult> PostDataPoint(string userId, string routeId,
        [FromBody] RouteDataPointInput[] data,
        [FromServices] IDataIngestionService ingestionService)
    {
        await ingestionService.Ingest(userId, routeId, data);
        return Ok();
    }
}