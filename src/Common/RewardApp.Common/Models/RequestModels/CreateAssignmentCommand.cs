using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Common.Models.RequestModels;

public class CreateAssignmentCommand : IRequest<Guid>
{
    public CreateAssignmentCommand(DateTime createDate, Guid? rewardId, Guid? userId, string assignmentName, sbyte state)
    {
        CreateDate = createDate;
        RewardId = rewardId;
        UserId = userId;
        AssignmentName = assignmentName;
        State = state;
    }

    public DateTime CreateDate { get; set; }
    public Guid? RewardId { get; set; }
    public Guid? UserId { get; set; }
    public string AssignmentName { get; set; }
    public sbyte State { get; set; }
}
