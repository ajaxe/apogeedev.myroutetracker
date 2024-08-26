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
            Latitude = data.Geometry?.Coordinates[1].ToString(), // Latitude
            Longitude = data.Geometry?.Coordinates[0].ToString(), //Longitude,
            Speed = data.Properties?.Speed.ToString(),
            SpeedUnit = data.Properties?.SpeedUnit,
            Heading = data.Properties?.Heading.ToString(),
        };
    }
}