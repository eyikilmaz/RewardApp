using MediatR;
using Microsoft.AspNetCore.Mvc;
using RewardApp.Common.Models.RequestModels;
using RewardApp.Common.Models.RequestModels.RewardUser;

namespace RewardApp.Api.WebApi.Controllers;

[Route("api/rewarduser")]
[ApiController]
public class RewardUserController : BaseController
{
    private readonly IMediator mediator;
    public RewardUserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromBody] CreateRewardUserCommand command)
    {
        var result = await mediator.Send(command);

        return Ok(result);
    }
    
    [HttpPost]
    [Route("openreward/{userid}")]
    public async Task<IActionResult> OpenReward(Guid userid, Guid uid)
    {
        var result = await mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId, voteType, UserId.Value));

        //var result = await mediator.Send(command);

        return Ok(result);
    }
}
