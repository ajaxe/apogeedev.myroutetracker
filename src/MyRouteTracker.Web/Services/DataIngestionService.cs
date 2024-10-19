using MongoDB.Bson;
using MyRouteTracker.Web.Abstractions;
using MyRouteTracker.Web.Abstractions.Services;
using static MyRouteTracker.Web.Database.Models.RouteDataPoint;

namespace MyRouteTracker.Web.Services;

public class DataIngestionService : IDataIngestionService
{
    private readonly ILogger<DataIngestionService> logger;
    private readonly AppDbContext dbContext;
    private readonly IUserContextProvider userContextProvider;

    public DataIngestionService(AppDbContext dbContext,
        IUserContextProvider userContextProvider,
        ILogger<DataIngestionService> logger)
    {
        this.dbContext = dbContext;
        this.userContextProvider = userContextProvider;
        this.logger = logger;
    }
    public async Task Ingest(string routeId, RouteDataPointInput[] dataPoint)
    {

        var profile = await userContextProvider.GetUserProfile()
            ?? throw new InvalidOperationException("Invalid user context");

        logger.LogInformation("{@UserId} {@RouteId} posted {@DataPoint}",
            profile.UserIdentifier, routeId, dataPoint);
        // check userId exists
        // check routeId exists

        var dps = dataPoint.Where(d => d.Coords != null
                && d.Coords.Longitude.HasValue
                && d.Coords.Latitude.HasValue)
            .Select(d => new RouteDataPoint
            {
                Geometry = new GeometryData
                {
                    Coordinates = [d.Coords!.Longitude!.Value, d.Coords.Latitude!.Value],
                },
                Timestamp = d.Timestamp,
                RouteDataSetId = ObjectId.Parse(routeId),
                UserProfileId = profile.UserIdentifier,
                Properties = new DataPointProperties
                {
                    Heading = d.Coords.Heading,
                    Speed = d.Coords.Speed,
                }
            });

        if (!dps.Any())
        {
            return;
        }

        await dbContext.AddRangeAsync(dps);
        await dbContext.SaveChangesAsync();
    }
}