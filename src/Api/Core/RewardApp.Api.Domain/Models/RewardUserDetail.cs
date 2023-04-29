using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Domain.Models;

public class RewardUserDetail
{
    public Guid Uid { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid CurrentRewardId { get; set; }
    public bool IsOpen { get; set; }
    public sbyte Mod { get; set; }
    public string Description { get; set; }
    public Guid LastRewardId { get; set; }
}
