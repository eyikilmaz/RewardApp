using MediatR;
using RewardApp.Common.Models.Queries.Assignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Queries.Assignment;

public class GetRewardDetailQuery : IRequest<AssignmentDetailViewModel>
{
    public GetRewardDetailQuery(Guid? userId)
    {
        UserId = userId;
    }

    public Guid? UserId { get; set; }
}
