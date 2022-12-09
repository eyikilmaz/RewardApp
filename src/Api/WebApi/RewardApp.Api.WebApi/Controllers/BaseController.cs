using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RewardApp.Api.WebApi.Controllers;
[Route("api/[Controller]")]
[ApiController]

public class BaseController : ControllerBase
{
    public Guid? UserId
    {
        get
        {
            var val = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return val is null ? null : new Guid(val);
        }
    }
}
