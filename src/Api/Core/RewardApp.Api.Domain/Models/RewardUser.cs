using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RewardApp.Api.Domain.Models;

public class RewardUser : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    [Column(TypeName = "jsonb")]
    public List<RewardUserDetail> RewardUserDetails { get; set; }
}