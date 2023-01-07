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

public class WinnerEntityConfiguration : BaseEntityConfiguration<Winner>
{
    public override void Configure(EntityTypeBuilder<Winner> builder)
    {
        base.Configure(builder);

        builder.ToTable("winner", RewardAppContext.DEFAULT_SCHEMA);
    }
}
