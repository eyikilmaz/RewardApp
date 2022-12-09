using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Common.Models.RequestModels;

public class ConfirmEmailCommand : IRequest<bool>
{
    // TODO: bütün command'lar eğer ui ile ortak kullanılmayacaksa application features altına taşınabilir. Aynı querieslerde olduğu gibi.
    public Guid ConfirmationId { get; set; }
}
