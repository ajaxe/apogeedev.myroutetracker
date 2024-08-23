using MyRouteTracker.Web.Abstractions.Services;

namespace MyRouteTracker.Web.Services;

public class DataIngestionService : IDataIngestionService
{
    private readonly ILogger<DataIngestionService> logger;

    public DataIngestionService(ILogger<DataIngestionService> logger)
    {
        this.logger = logger;
    }
    public Task Ingest(string userId, string routeId, RouteDataPointInput[] dataPoint)
    {
        logger.LogInformation("{@UserId} {@RouteId} posted {@DataPoint}",
            userId, routeId, dataPoint);
        return Task.CompletedTask;
    }
}