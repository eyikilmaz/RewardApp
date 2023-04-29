using MediatR;
using Microsoft.AspNetCore.Mvc;
using RewardApp.Api.Application.Features.Command.Assignment.Delete;
using RewardApp.Api.Application.Features.Queries.Assignment;
using RewardApp.Api.Application.Features.Queries.User.GetUserDetail;
using RewardApp.Common.Models.Queries.Assignment;
using RewardApp.Common.Models.RequestModels;

namespace RewardApp.Api.WebApi.Controllers;
[Route("api/assignments")]
[ApiController]


public class AssignmentController : BaseController
{
    private readonly IMediator mediator;
    public AssignmentController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{userid}")]
    public async Task<IActionResult> Get(Guid userid)
    {
        var user = await mediator.Send(new GetAssignmentDetailQuery(userid));

        return Ok(user);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create([FromBody] CreateAssignmentCommand command)
    {
        if (!command.UserId.HasValue)
            command.UserId = UserId;

        var result = await mediator.Send(command);

        return Ok(result);
    }

    [HttpPost]
    [Route("delete/{assignmentId}")]
    public async Task<IActionResult> Delete(Guid assignmentId)
    {
        var result = await mediator.Send(new DeleteAssignmentCommand(assignmentId, UserId.Value));

        return Ok(result);
    }

    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] UpdateAssignmentCommand command)
    {
        var result = await mediator.Send(command);

        // master merge before

        return Ok(result);
    }
}
