using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Common.Models.RequestModels;

public class UpdateAssignmentCommand : IRequest<Guid>
{
    public UpdateAssignmentCommand(Guid id, DateTime createDate, Guid? rewardId, Guid? userId, string assignmentName, sbyte state)
    {
        Id = id;
        CreateDate = createDate;
        RewardId = rewardId;
        UserId = userId;
        AssignmentName = assignmentName;
        State = state;
    }

    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid? RewardId { get; set; }
    public Guid? UserId { get; set; }
    public string AssignmentName { get; set; }
    public sbyte State { get; set; }
}
