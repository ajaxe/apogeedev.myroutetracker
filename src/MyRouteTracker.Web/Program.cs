using MyRouteTracker.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("secrets.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables(prefix: Startup.EnvVarPrefix);

var startup = new Startup(builder.Configuration, builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app);

app.Run();
