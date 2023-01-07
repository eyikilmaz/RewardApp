﻿using Microsoft.EntityFrameworkCore;
using RewardApp.Api.Application.Interfaces.Repositories;
using RewardApp.Api.Domain.Models;
using RewardApp.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Infrastructure.Persistence.Repositories;

public class RewardRepository : GenericRepository<Reward>, IRewardRepository
{
    public RewardRepository(RewardAppContext dbContext) : base(dbContext)
    {
    }
}
