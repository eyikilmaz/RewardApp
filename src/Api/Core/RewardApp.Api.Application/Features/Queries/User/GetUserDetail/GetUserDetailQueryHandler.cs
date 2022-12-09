﻿using AutoMapper;
using MediatR;
using RewarApp.Api.Application.Interfaces.Repositories;
using RewardApp.Common.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Queries.User.GetUserDetail;

public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        Domain.Models.User dbUser = null;

        if (request.UserId != Guid.Empty)
        {
            dbUser = await userRepository.GetByIdAsync(request.UserId);
        }
        else if (!string.IsNullOrEmpty(request.UserName))
        {
            dbUser = await userRepository.GetSingleAsync(i => i.UserName == request.UserName);
        }

        return mapper.Map<UserDetailViewModel>(dbUser);
    }
}