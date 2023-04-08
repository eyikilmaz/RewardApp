using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Common.Models.Queries.Reward;

public class RewardDetailViewModel
{
    public Guid Id { get; set; }

    public DateTime CreateDate { get; set; }
    public Guid RewardKey { get; set; }

    public string RewardName { get; set; }

    public bool IsDefault { get; set; }
    public sbyte Repeat { get; set; }
    public sbyte Mod { get; set; }
    public string Description { get; set; }
    public string Logo { get; set; }
}
