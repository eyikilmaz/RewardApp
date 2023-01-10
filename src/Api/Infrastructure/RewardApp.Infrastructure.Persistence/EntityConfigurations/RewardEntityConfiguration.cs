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

public class RewardEntityConfiguration : BaseEntityConfiguration<Reward>
{
    public override void Configure(EntityTypeBuilder<Reward> builder)
    {
        base.Configure(builder);

        builder.ToTable("reward", RewardAppContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.CreatedBy)
             .WithMany(i => i.Rewards)
             .HasForeignKey(i => i.CreatedById);
    }
}
