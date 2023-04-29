using AutoMapper;
using MediatR;
using RabbitMQ.Client;
using RewardApp.Api.Application.Interfaces.Repositories;
using RewardApp.Common.Models.RequestModels;
using RewardApp.Common.Models.RequestModels.RewardUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.RewardUser.Create;

public class CreateRewardUserCommandHandler : IRequestHandler<CreateRewardUserCommand, Guid>
{
    private readonly IRewardUserRepository rewardUserRepository;
    private readonly IMapper mapper;

    public CreateRewardUserCommandHandler(IRewardUserRepository rewardUserRepository, IMapper mapper)
    {
        this.rewardUserRepository = rewardUserRepository;
        this.mapper = mapper;
    }
   
    public async Task<Guid> Handle(CreateRewardUserCommand request, CancellationToken cancellationToken)
    {
        var dbRewardUser = mapper.Map<Domain.Models.RewardUser>(request);

        await rewardUserRepository.AddAsync(dbRewardUser);

        return dbRewardUser.UserId;
    }
}
