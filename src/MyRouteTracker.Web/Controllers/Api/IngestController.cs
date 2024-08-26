using Microsoft.AspNetCore.Mvc;
using MyRouteTracker.Web.Abstractions.Services;

namespace MyRouteTracker.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class IngestController : ControllerBase
{
    private readonly ILogger<IngestController> logger;

    public IngestController(ILogger<IngestController> logger)
    {
        this.logger = logger;
    }

    [HttpPost("{userId}/{routeId}/datapoint")]
    public async Task<IActionResult> PostDataPoint(string userId, string routeId,
        [FromBody] RouteDataPointInput[] data,
        [FromServices] IDataIngestionService ingestionService)
    {
        await ingestionService.Ingest(userId, routeId, data);
        return NoContent();
    }
    [HttpPost("errors")]
    public IActionResult PostErrors([FromBody] Dictionary<string, object> data)
    {
        logger.LogError("JS {@Error}", data);
        return NoContent();
    }
}