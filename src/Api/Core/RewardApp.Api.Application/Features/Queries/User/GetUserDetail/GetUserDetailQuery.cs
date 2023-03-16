using MediatR;
using RewardApp.Common.Models.Queries.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Queries.User.GetUserDetail;

public class GetUserDetailQuery : IRequest<UserDetailViewModel>
{
    // branch emre değişikliği
    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public GetUserDetailQuery(Guid userId, string userName = null)
    {
        UserId = userId;
        UserName = userName;
    }
}
