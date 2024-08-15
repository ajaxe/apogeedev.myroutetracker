namespace MyRouteTracker.Web.Helpers.Configuration;

public class AppOptions
{
    public const string SectionName = nameof(AppOptions);
    public string MongoDbConnection { get; set; } = default!;
    public string DatabaseName { get; set; } = default!;
}