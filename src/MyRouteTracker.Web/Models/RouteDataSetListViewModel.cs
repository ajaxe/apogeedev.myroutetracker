namespace MyRouteTracker.Web.Models;

public class RouteDataSetListViewModel
{
    public List<RouteDataSetViewModel> Routes { get; set; } = new List<RouteDataSetViewModel>();
}
public class RouteDataSetViewModel
{
    public string Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Mode { get; set; }

    public static explicit operator RouteDataSetViewModel(RouteDataSet model)
    {
        return new RouteDataSetViewModel
        {
            Id = model.Id.ToString(),
            Name = model.Name,
            Mode = model.Mode,
        };
    }
}