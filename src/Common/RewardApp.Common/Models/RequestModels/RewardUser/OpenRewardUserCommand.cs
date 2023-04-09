using MediatR;
using RewardApp.Common.Models.Queries.RewardUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Common.Models.RequestModels.RewardUser;

public class OpenRewardUserCommand : IRequest<OpenRewardUserViewModel>
{
    public OpenRewardUserCommand(Guid userId, Guid uId)
    {
        UserId = userId;
        UId = uId;
    }

    public OpenRewardUserCommand()
    {

    }
    public Guid UserId { get; set; }

    public Guid UId { get; set; }
}
