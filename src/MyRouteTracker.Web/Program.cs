using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using MyRouteTracker.Web.Abstractions;
using MyRouteTracker.Web.Abstractions.Services;
using MyRouteTracker.Web.Helpers.Configuration;
using MyRouteTracker.Web.Services;
using Serilog;

const string EnvVarPrefix = "APP_";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog((s, lc) => lc.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<AppOptions>(
    builder.Configuration.GetSection(AppOptions.SectionName)
);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddOptions();
builder.Configuration
    .AddJsonFile("secrets.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables(prefix: EnvVarPrefix);

builder.Services.AddScoped<IUserContextProvider, UserContextProvider>();
builder.Services.AddTransient<IRouteDataService, RouteDataService>();
builder.Services.AddTransient<IDataIngestionService, DataIngestionService>();

builder.Services.AddDbContext<AppDbContext>((sp, o) =>
    {
        var opts = sp.GetRequiredService<IOptionsSnapshot<AppOptions>>().Value;
        o.UseMongoDB(opts.MongoDbConnection, opts.DatabaseName)
            .UseCamelCaseNamingConvention();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
