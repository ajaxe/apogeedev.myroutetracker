using System.Text.Json.Serialization;
using MyRouteTracker.Web.Helpers;

namespace MyRouteTracker.Web.Abstractions.Services;

public interface IDataIngestionService
{
    Task Ingest(string routeId, RouteDataMark dataMark);
}
public class RouteDataMark
{
    public RouteDataMark()
    {
        DataPoints = Enumerable.Empty<RouteDataPointInput>().ToArray();
    }
    public RouteDataPointInput[] DataPoints { get; set; }
    public decimal BatteryLevel { get; set; } = 1;
}
public class RouteDataPointInput
{
    [JsonConverter(typeof(MillisecondEpochConverter))]
    public DateTime Timestamp { get; set; }

    public CoordinateData? Coords { get; set; }
    public class CoordinateData
    {
        public string? DataType { get; set; } = "Point";
        public double? Accuracy { get; set; }
        public decimal? Altitude { get; set; }
        public double? AltitudeAccuracy { get; set; }
        public decimal? Heading { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Speed { get; set; }
    }
}

