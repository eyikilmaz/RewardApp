using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Common.Models.Queries.Assignment;

public class AssignmentDetailViewModel
{
    public Guid Id { get; set; }

    public DateTime CreateDate { get; set; }

    public string RewardName { get; set; }

    public string AssignmentName { get; set; }

    public int State { get; set; }
}
