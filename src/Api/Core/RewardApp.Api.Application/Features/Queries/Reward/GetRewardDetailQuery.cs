using MediatR;
using RewardApp.Common.Models.Queries.Assignment;
using RewardApp.Common.Models.Queries.Reward;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Queries.Assignment;

public class GetRewardDetailQuery : IRequest<RewardDetailViewModel>
{
    public GetRewardDetailQuery(Guid? rewardId)
    {
        RewardId = rewardId;
    }

    public Guid? RewardId { get; set; }
}
