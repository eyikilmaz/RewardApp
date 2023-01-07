using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Domain.Models;

public class Assignment : BaseEntity
{
    public DateTime CreateDate { get; set; }
    public Reward RewardId { get; set; }
    public User UserId { get; set; }
    public string AssignmentName { get; set; }
    public sbyte State { get; set; }
}
