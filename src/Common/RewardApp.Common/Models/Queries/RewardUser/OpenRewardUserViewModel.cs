using RewardApp.Api.Domain.Models;
using RewardApp.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Common.Models.Queries.RewardUser;

public class OpenRewardUserViewModel
{
    public GameResult GameResult { get; set; }
    public List<RewardUserDetail> RewardUserDetails { get; set; }
}
