using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyRouteTracker.Web.Models;

namespace MyRouteTracker.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index([FromServices] AppDbContext dbContext)
    {
        await SeedDb(dbContext);

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task SeedDb(AppDbContext dbContext)
    {
        var defaultUser = new UserProfile
        {
            AuthenticationType = Guid.NewGuid().ToString(),
            ExternalId = "default",
        };

        if (!await dbContext.UserProfiles.AnyAsync(p => p.ExternalId == "default"))
        {
            dbContext.UserProfiles.Add(defaultUser);

            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.RouteDataSets.AnyAsync(p => p.UserProfileId == defaultUser.Id))
        {
            dbContext.RouteDataSets.Add(new RouteDataSet
            {
                Name = "Default route set",
                Mode = "Walk",
                UserProfileId = defaultUser.Id,
            });
            await dbContext.SaveChangesAsync();
        }
    }
}
