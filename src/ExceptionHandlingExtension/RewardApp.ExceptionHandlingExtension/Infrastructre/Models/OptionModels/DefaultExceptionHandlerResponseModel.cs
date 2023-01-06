using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.ExceptionHandlingExtension.Infrastructre.Models.OptionModels;

public class DefaultExceptionHandlerResponseModel
{
    public string Detail { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public DefaultExceptionHandlerResponseModel()
    {
        StatusCode = HttpStatusCode.InternalServerError;
    }
}
