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

public class AssignmentEntityConfiguration : BaseEntityConfiguration<Assignment>
{
    public override void Configure(EntityTypeBuilder<Assignment> builder)
    {
        base.Configure(builder);

        builder.ToTable("assignment", RewardAppContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.User)
               .WithMany(i => i.Assignments)
               .HasForeignKey(i => i.UserId)
               .OnDelete(DeleteBehavior.Restrict);

    }
}
