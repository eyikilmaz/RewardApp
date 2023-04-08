using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.Assignment.Delete;

public class DeleteRewardCommand : IRequest<bool>
{
    public DeleteRewardCommand(Guid rewardId)
    {
        RewardId = rewardId;
    }

    public Guid RewardId { get; set; }
}
