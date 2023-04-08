using AutoMapper;
using MediatR;
using RewardApp.Api.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.Assignment.Delete;

public class DeleteRewardCommandHandler : IRequestHandler<DeleteRewardCommand, bool>
{
    private readonly IRewardRepository rewardRepository;
    private readonly IMapper mapper;

    public DeleteRewardCommandHandler(IRewardRepository rewardRepository, IMapper mapper)
    {
        this.rewardRepository = rewardRepository;
        this.mapper = mapper;
    }

    public async Task<bool> Handle(DeleteRewardCommand request, CancellationToken cancellationToken)
    {
        var dbAssignment = await rewardRepository.GetByIdAsync(request.RewardId);
        await rewardRepository.DeleteAsync(dbAssignment);

        return true;
    }
}
