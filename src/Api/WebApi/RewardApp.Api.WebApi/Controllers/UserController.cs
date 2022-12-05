using MediatR;
using Microsoft.AspNetCore.Mvc;
using RewardApp.Common.Models.RequestModels;

namespace RewardApp.Api.WebApi.Controllers;
[Route("api/[Controller]")]
[ApiController]

public class UserController : Controller
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var res = await mediator.Send(command);

        return Ok(res);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var res = await mediator.Send(command);

        return Ok(res);
    }

    [HttpPost]
    [Route("Update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
    {
        var res = await mediator.Send(command);

        return Ok(res);
    }
}
