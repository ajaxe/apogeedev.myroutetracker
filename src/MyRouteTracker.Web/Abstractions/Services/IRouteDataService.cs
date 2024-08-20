namespace MyRouteTracker.Web.Abstractions.Services;

public interface IRouteDataService
{
    Task<IEnumerable<RouteDataSet>> GetRoutes(bool? deleted = false);
    Task<RouteDataSet?> GetRoute(string routeDateSetId);
    Task<RouteDataSet> CreateNewRoute();
    Task DeleteRoute(string trackerId);
}