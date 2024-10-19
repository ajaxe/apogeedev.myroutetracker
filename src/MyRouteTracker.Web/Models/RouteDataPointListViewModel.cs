namespace MyRouteTracker.Web.Models;

public class RouteDataPointListViewModel
{
    public RouteDataPointListViewModel()
    {
        DataPoints = new List<RouteDataPointViewModel>();
    }
    public List<RouteDataPointViewModel> DataPoints { get; set; }

}
public class RouteDataPointViewModel
{
    public RouteDataPointViewModel() { }
    public RouteDataPointViewModel(RouteDataPoint data, string? unit)
    {
        var factor = unit == "mph" ? 0.44704M : 18 / 5;
        Id = data.Id.ToString();
        RecordDate = data.Timestamp.GetValueOrDefault(data.InsertDate.GetValueOrDefault());
        Latitude = data.Geometry?.Coordinates[1].ToString("0.000"); // Latitude
        Longitude = data.Geometry?.Coordinates[0].ToString("0.000"); //Longitude,
        Speed = (data.Properties!.Speed.GetValueOrDefault() / factor).ToString("0.000");
        SpeedUnit = data.Properties?.SpeedUnit;
        Heading = data.Properties?.Heading?.ToString("0.000");
    }

    public string Id { get; set; } = default!;
    public DateTime RecordDate { get; set; } = default!;
    public string? Longitude { get; set; }
    public string? Latitude { get; set; }
    public string? Speed { get; set; }
    public string? SpeedUnit { get; set; }
    public string? Heading { get; set; }

    public static explicit operator RouteDataPointViewModel(RouteDataPoint data)
    {
        return new RouteDataPointViewModel
        {
            Id = data.Id.ToString(),
            RecordDate = data.Timestamp.GetValueOrDefault(data.InsertDate.GetValueOrDefault()),
            Latitude = data.Geometry?.Coordinates[1].ToString("0.000"), // Latitude
            Longitude = data.Geometry?.Coordinates[0].ToString("0.000"), //Longitude,
            Speed = data.Properties?.Speed?.ToString("0.000"),
            SpeedUnit = data.Properties?.SpeedUnit,
            Heading = data.Properties?.Heading?.ToString("0.000"),
        };
    }
}