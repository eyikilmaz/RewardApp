using MediatR;
using RewardApp.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Common.Models.RequestModels.RewardUser;

public class CreateRewardUserCommand : IRequest<Guid>
{
    public CreateRewardUserCommand(Guid? userId, List<RewardUserDetail> rewardUserDetails, Guid? rewardId)
    {
        UserId = userId;
        RewardUserDetails = rewardUserDetails;
        RewardId = rewardId;
    }

    public Guid? UserId { get; set; }
    public List<RewardUserDetail> RewardUserDetails { get; set; }
    public Guid? RewardId { get; set; }
}
