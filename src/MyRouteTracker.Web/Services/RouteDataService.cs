using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MyRouteTracker.Web.Abstractions;
using MyRouteTracker.Web.Abstractions.Services;

namespace MyRouteTracker.Web.Services;

public class RouteDataService : IRouteDataService
{
    private readonly IUserContextProvider userContext;
    private readonly AppDbContext dbContext;

    public RouteDataService(IUserContextProvider userContext,
        AppDbContext dbContext)
    {
        this.userContext = userContext;
        this.dbContext = dbContext;
    }
    public async Task<IEnumerable<RouteDataSet>> GetRoutes()
    {
        var user = await userContext.GetUserProfile()
            ?? throw new InvalidOperationException("Invalid user");

        return await dbContext.RouteDataSets
            .Where(r => r.UserProfileId == user.Id)
            .ToListAsync();
    }
    public async Task<RouteDataSet?> GetRoute(string routeDateSetId)
    {
        var id = new ObjectId(routeDateSetId);
        return await dbContext.RouteDataSets.FirstOrDefaultAsync(p => p.Id == id);
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
}