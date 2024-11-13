using Microsoft.AspNetCore.Mvc;
using MyRouteTracker.Web.Abstractions;
using MyRouteTracker.Web.Models;

namespace MyRouteTracker.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSession([FromServices] IUserContextProvider userInfo)
    {
        var profile = await userInfo.GetUserProfile();
        var vm = profile != null ? (SessionViewModel)profile : new SessionViewModel();

        vm.LoginUrl = Url.RouteUrl("login");
        vm.LogoutUrl = Url.RouteUrl("logout");

        return Ok(vm);
    }
}