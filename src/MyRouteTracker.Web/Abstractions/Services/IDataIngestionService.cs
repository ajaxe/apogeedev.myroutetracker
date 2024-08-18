using MyRouteTracker.Web.Models;

namespace MyRouteTracker.Web.Abstractions.Services;

public interface IDataIngestionService
{
    Task Ingest(string userId, string routeId, RouteDataPointInput dataPoint);
}
public class RouteDataPointInput
{
    public CoordinateData Geometry { get; set; }
    public string CurrentSpeed { get; set; }
    public string CurrentDirection { get; set; }
    public class CoordinateData
    {
        public string DataType { get; set; } = "Point";
        public int[] Coordinates { get; set; }
    }
}

