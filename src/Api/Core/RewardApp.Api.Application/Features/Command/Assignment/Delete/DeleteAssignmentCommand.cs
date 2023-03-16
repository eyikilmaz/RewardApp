using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.Assignment.Delete;

public class DeleteAssignmentCommand : IRequest<bool>
{
    public DeleteAssignmentCommand(Guid assignmentId, Guid? userId)
    {
        AssignmentId = assignmentId;
        UserId = userId;
    }

    public Guid AssignmentId { get; set; }
    public Guid? UserId { get; set; }
}
