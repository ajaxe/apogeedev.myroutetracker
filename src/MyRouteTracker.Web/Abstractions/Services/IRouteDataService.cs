namespace MyRouteTracker.Web.Abstractions.Services;

public interface IRouteDataService
{
    Task<IEnumerable<RouteDataSet>> GetRoutes();
    Task<RouteDataSet?> GetRoute(string routeDateSetId);
    Task<RouteDataSet> CreateNewRoute();
}