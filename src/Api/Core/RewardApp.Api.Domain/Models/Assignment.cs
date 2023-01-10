using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Domain.Models;

public class Assignment : BaseEntity
{
    public DateTime CreateDate { get; set; }
    public Guid RewardId { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public virtual Reward Reward { get; set; }
    public string AssignmentName { get; set; }
    public sbyte State { get; set; }
}
