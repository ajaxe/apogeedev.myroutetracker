using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MyRouteTracker.Web.Abstractions;
using MyRouteTracker.Web.Abstractions.Services;

namespace MyRouteTracker.Web.Services;

public class RouteDataService : IRouteDataService
{
    private readonly IUserContextProvider userContext;
    private readonly AppDbContext dbContext;
    private readonly ILogger<RouteDataService> logger;

    public RouteDataService(IUserContextProvider userContext,
        AppDbContext dbContext, ILogger<RouteDataService> logger)
    {
        this.userContext = userContext;
        this.dbContext = dbContext;
        this.logger = logger;
    }
    public async Task<IEnumerable<RouteDataSet>> GetRoutes(bool? deleted = false)
    {
        var user = await userContext.GetUserProfile()
            ?? throw new InvalidOperationException("Invalid user");

        var q = dbContext.RouteDataSets
            .Where(r => r.UserProfileId == user.Id);

        if (deleted.HasValue)
        {
            q = q.Where(r => !r.MarkForDeletion.HasValue || r.MarkForDeletion == deleted);
        }

        var r = await q.ToListAsync();

        logger.LogInformation("GetRoutes: {@user} {@count}", user.Id.ToString(), r.Count());

        return r;
    }
    public async Task<RouteDataSet?> GetRoute(string routeDateSetId)
    {
        return await dbContext.RouteDataSets.FirstOrDefaultAsync(p => p.Id == routeDateSetId);
    }
    public async Task<IEnumerable<RouteDataPoint>> GetRouteDataPoints(string routeDateSetId)
    {
        var id = ObjectId.Parse(routeDateSetId);
        return await dbContext.RouteDataPoints.Where(p => p.RouteDataSetId == id).ToListAsync();
    }
    public async Task<RouteDataSet> CreateNewRoute()
    {
        var user = await userContext.GetUserProfile()
            ?? throw new InvalidOperationException("Invalid user");

        var created = new RouteDataSet
        {
            UserProfileId = user.Id,
            Name = $"Route {DateTime.Now}",
            Mode = "Walk",
        };

        await dbContext.RouteDataSets.AddAsync(created);
        await dbContext.SaveChangesAsync();

        return created;
    }
    public async Task DeleteRoute(string trackerId)
    {
        if (string.IsNullOrWhiteSpace(trackerId))
        {
            logger.LogInformation("Invalid tracker id, skipping delete");
            return;
        }

        var routeData = await dbContext.RouteDataSets.FirstOrDefaultAsync(r => r.Id == trackerId);

        if (routeData is not null)
        {
            routeData.MarkForDeletion = true;
            await dbContext.SaveChangesAsync();
        }
    }
}