using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using RewarApp.Api.Application.Interfaces.Repositories;
using RewardApp.Api.Application.Interfaces.Repositories;
using RewardApp.Api.Domain.Models;
using RewardApp.Common.Models.Queries.RewardUser;
using RewardApp.Common.Models.RequestModels.RewardUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RewardApp.Common.Enums;

namespace RewardApp.Api.Application.Features.Command.RewardUser.Open;

public class OpenRewardUserCommandHandler : IRequestHandler<OpenRewardUserCommand, OpenRewardUserViewModel>
{
    private readonly IMapper mapper;
    private readonly IRewardUserRepository rewardUserRepository;
    private readonly IRewardRepository rewardRepository;
    public OpenRewardUserCommandHandler(IMapper mapper, IRewardUserRepository rewardUserRepository, IRewardRepository rewardRepository)
    {
        this.mapper = mapper;
        this.rewardUserRepository = rewardUserRepository;
        this.rewardRepository = rewardRepository;
    }

    public async Task<OpenRewardUserViewModel> Handle(OpenRewardUserCommand request, CancellationToken cancellationToken)
    {
        var existsRewardUser = rewardUserRepository.GetList(i => i.UserId == request.UserId).Result.FirstOrDefault();

        if (existsRewardUser is null)
            return new OpenRewardUserViewModel() { GameResult = GameResult.Continue, RewardUserDetails = null };

        var selectedReward = existsRewardUser.RewardUserDetails.Where(r => r.Uid == request.UId).FirstOrDefault();
        var openRewards = existsRewardUser.RewardUserDetails.Where(r => r.IsOpen).ToList();

        if (!openRewards.Any())
        {
            foreach (var item in existsRewardUser.RewardUserDetails)
            {
                if (item.Uid == selectedReward.Uid)
                    item.IsOpen = true;

                item.Uid = Guid.NewGuid();
                item.LastRewardId = selectedReward.CurrentRewardId;
            }

            await rewardUserRepository.UpdateAsync(existsRewardUser);

            return new OpenRewardUserViewModel() { GameResult = GameResult.Continue, RewardUserDetails = existsRewardUser.RewardUserDetails };
        }

        var bomb = rewardRepository.GetList(i => i.RewardName == "Bomb").Result.FirstOrDefault();

        if (selectedReward.LastRewardId == selectedReward.CurrentRewardId) // same reward
        {
            if (selectedReward.CurrentRewardId == bomb.Id)
            {
                await OpenTrueAndUpdate(existsRewardUser, selectedReward);
                return new OpenRewardUserViewModel() { GameResult = GameResult.Continue, RewardUserDetails = existsRewardUser.RewardUserDetails };
            }
            var winCount = openRewards.Where(i => i.CurrentRewardId == selectedReward.CurrentRewardId).Count();

            if (winCount + 1 == selectedReward.Mod)
            {
                await OpenTrueAndUpdate(existsRewardUser, selectedReward, false);
                await MixAndUpdate(rewardUser: existsRewardUser);
                return new OpenRewardUserViewModel() { GameResult = GameResult.Win, RewardUserDetails = existsRewardUser.RewardUserDetails };
            }
        }
        else
        {
            if (selectedReward.CurrentRewardId != bomb.Id)
            {
                await OpenTrueAndUpdate(existsRewardUser, selectedReward, false);
                await MixAndUpdate(rewardUser: existsRewardUser);
                return new OpenRewardUserViewModel() { GameResult = GameResult.Lose, RewardUserDetails = existsRewardUser.RewardUserDetails };
            }
              

            await OpenTrueAndUpdate(existsRewardUser, selectedReward, false);
            await MixAndUpdate(rewardUser: existsRewardUser);
            return new OpenRewardUserViewModel() { GameResult = GameResult.Continue, RewardUserDetails = existsRewardUser.RewardUserDetails };
        }

        return new OpenRewardUserViewModel() { GameResult = GameResult.Continue, RewardUserDetails = existsRewardUser.RewardUserDetails };
    }

    private async Task OpenTrueAndUpdate(Domain.Models.RewardUser rewardUser, RewardUserDetail selectedReward, bool dbUpdate = true)
    {
        foreach (var item in rewardUser.RewardUserDetails)
        {
            if (item.Uid == selectedReward.Uid)
            {
                item.IsOpen = true;
                break;
            }
        }

        if (dbUpdate)
            await rewardUserRepository.UpdateAsync(rewardUser);
    }

    private async Task MixAndUpdate(Domain.Models.RewardUser rewardUser)
    {
        Shuffle(rewardUser.RewardUserDetails);
        UpdateUid(rewardUser.RewardUserDetails);

        await rewardUserRepository.UpdateAsync(rewardUser);
    }

    private void Shuffle<T>(IList<T> list)
    {
        Random rng = new Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void UpdateUid<T>(IList<T> list)
    {
        var rud = list as List<RewardUserDetail>;
        foreach (var item in rud)
        {
            item.Uid = Guid.NewGuid();
        }
    }
}
