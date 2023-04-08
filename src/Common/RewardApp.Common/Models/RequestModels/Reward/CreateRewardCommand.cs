using MediatR;
using RewardApp.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Common.Models.RequestModels.Reward;

public class CreateRewardCommand : IRequest<Guid>
{
    public CreateRewardCommand()
    {
            
    }
    public CreateRewardCommand(Guid rewardKey, DateTime createDate, Guid createdById, string rewardName, bool ısDefault, sbyte repeat, sbyte mod, string description, string logo)
    {
        RewardKey = rewardKey;
        CreateDate = createDate;
        CreatedById = createdById;
        RewardName = rewardName;
        IsDefault = ısDefault;
        Repeat = repeat;
        Mod = mod;
        Description = description;
        Logo = logo;
    }

    public Guid RewardKey { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid CreatedById { get; set; }
    public string RewardName { get; set; }
    public bool IsDefault { get; set; }
    public sbyte Repeat { get; set; }
    public sbyte Mod { get; set; }
    public string Description { get; set; }
    public string Logo { get; set; }
}
