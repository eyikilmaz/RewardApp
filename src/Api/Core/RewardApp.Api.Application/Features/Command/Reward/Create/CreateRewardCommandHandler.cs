using AutoMapper;
using MediatR;
using RabbitMQ.Client;
using RewardApp.Api.Application.Interfaces.Repositories;
using RewardApp.Common.Exceptions;
using RewardApp.Common.Models.RequestModels;
using RewardApp.Common.Models.RequestModels.Reward;
using RewardApp.Common.Models.RequestModels.RewardUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.RewardUser.Create;

public class CreateRewardCommandHandler : IRequestHandler<CreateRewardCommand, Guid>
{
    private readonly IRewardRepository rewardRepository;
    private readonly IMapper mapper;

    public CreateRewardCommandHandler(IRewardRepository rewardRepository, IMapper mapper)
    {
        this.rewardRepository = rewardRepository;
        this.mapper = mapper;
    }
   
    public async Task<Guid> Handle(CreateRewardCommand request, CancellationToken cancellationToken)
    {
        var existsReward = await rewardRepository.GetSingleAsync(i => i.RewardName == request.RewardName);

        if (existsReward is not null)
            throw new DatabaseValidationException("Ödül zaten kayıtlı.");

        var dbReward = mapper.Map<Domain.Models.Reward>(request);

        await rewardRepository.AddAsync(dbReward);

        return dbReward.Id;
    }
}
