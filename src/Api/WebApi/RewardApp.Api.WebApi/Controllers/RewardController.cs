using MediatR;
using Microsoft.AspNetCore.Mvc;
using RewardApp.Api.Application.Features.Command.Assignment.Delete;
using RewardApp.Api.Application.Features.Queries.Assignment;
using RewardApp.Common.Models.RequestModels;
using RewardApp.Common.Models.RequestModels.Reward;
using RewardApp.Common.Models.RequestModels.RewardUser;

namespace RewardApp.Api.WebApi.Controllers;

[Route("api/reward")]
[ApiController]
public class RewardController : BaseController
{
    private readonly IMediator mediator;
    public RewardController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{rewardid}")]
    public async Task<IActionResult> Get(Guid rewardid)
    {
        var user = await mediator.Send(new GetRewardDetailQuery(rewardid));

        return Ok(user);
    }


    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromBody] CreateRewardCommand command)
    {
        var result = await mediator.Send(command);

        return Ok(result);
    }

    [HttpPost]
    [Route("delete/{rewardId}")]
    public async Task<IActionResult> Delete(Guid rewardId)
    {
        var result = await mediator.Send(new DeleteRewardCommand(rewardId));

        return Ok(result);
    }
}
