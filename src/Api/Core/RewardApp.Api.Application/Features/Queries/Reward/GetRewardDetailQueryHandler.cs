using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RewarApp.Api.Application.Interfaces.Repositories;
using RewardApp.Api.Application.Interfaces.Repositories;
using RewardApp.Api.Domain.Models;
using RewardApp.Common.Models.Queries.Assignment;
using RewardApp.Common.Models.Queries.Reward;
using RewardApp.Common.Models.Queries.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RewardApp.Api.Application.Features.Queries.Assignment;

public class GetRewardDetailQueryHandler
{
    private readonly IRewardRepository rewardRepository;
    private readonly IMapper mapper;

    public GetRewardDetailQueryHandler(IRewardRepository rewardRepository, IMapper mapper)
    {
        this.rewardRepository = rewardRepository;
        this.mapper = mapper;
    }

    public async Task<List<RewardDetailViewModel>> Handle(GetRewardDetailQuery request, CancellationToken cancellationToken)
    {
        var query = rewardRepository.GetAll();

        var list = mapper.Map<List<RewardDetailViewModel>>(query);
        
        return list;
    }
}
