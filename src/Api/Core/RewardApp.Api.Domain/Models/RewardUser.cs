using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Domain.Models;

public class RewardUser : BaseEntity
{
    public Guid Uid { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid UserId { get; set; }
    public Guid RewardId { get; set; }
    public virtual User User { get; set; }
    public virtual Reward Reward { get; set; }
    public bool IsOpen { get; set; }
    public sbyte Mod { get; set; }
    public string Description { get; set; }
    public Guid LastRewardId { get; set; }
}
