using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Domain.Models;

public class User : BaseEntity
{
    public DateTime CreateDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public bool EmailConfirmed { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; }
    public virtual ICollection<Reward> Rewards { get; set; }
    public virtual ICollection<RewardUser> RewardUsers { get; set; }
}
