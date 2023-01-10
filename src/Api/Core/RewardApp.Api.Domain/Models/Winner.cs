using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Domain.Models;

public class Winner : BaseEntity
{
    public DateTime CreateDate { get; set; }
    public Guid UserId { get; set; }
    public Guid RewardId { get; set; }
}
