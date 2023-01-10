using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RewardApp.Api.Domain.Models;
using RewardApp.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Infrastructure.Persistence.EntityConfigurations;

public class RewardUserEntityConfiguration : BaseEntityConfiguration<RewardUser>
{
    public override void Configure(EntityTypeBuilder<RewardUser> builder)
    {
        base.Configure(builder);

        builder.ToTable("rewarduser", RewardAppContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.Reward)
            .WithMany(i => i.RewardUsers)
            .HasForeignKey(i => i.RewardId);

        builder.HasOne(i => i.User)
          .WithMany(i => i.RewardUsers)
          .HasForeignKey(i => i.UserId)
          .OnDelete(DeleteBehavior.Restrict);
    }
}
