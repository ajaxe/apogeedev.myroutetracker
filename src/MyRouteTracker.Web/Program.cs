using MyRouteTracker.Web;

var builder = WebApplication.CreateBuilder(args);

var secretsFile = Environment.GetEnvironmentVariable("Secrets_File") ?? string.Empty;
var secretsOptional = false;

if (string.IsNullOrWhiteSpace(secretsFile))
{
    secretsFile = "secrets.json";
    secretsOptional = true;
}

builder.Configuration
    .AddJsonFile(secretsFile, optional: secretsOptional, reloadOnChange: true)
    .AddEnvironmentVariables(prefix: Startup.EnvVarPrefix);

var startup = new Startup(builder.Configuration, builder.Environment);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app);

app.Run();
