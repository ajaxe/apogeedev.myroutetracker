using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRouteTracker.Web.Abstractions;

namespace MyRouteTracker.Web.Controllers.Api;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSession([FromServices] IUserContextProvider userInfo)
    {
        return Ok(await userInfo.GetUserProfile() ?? new object());
    }
}