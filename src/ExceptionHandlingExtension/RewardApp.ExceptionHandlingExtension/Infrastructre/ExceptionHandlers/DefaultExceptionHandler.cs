using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RewardApp.ExceptionHandlingExtension.Infrastructre.Extensions;
using RewardApp.ExceptionHandlingExtension.Infrastructre.Models.OptionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.ExceptionHandlingExtension.Infrastructre.ExceptionHandlers;

internal class DefaultExceptionHandler
{
    internal static async Task Handle(HttpContext context,
                                      Exception exception,
                                      ILogger logger,
                                      bool useExceptionsDetails = false)
    {
        var res = new DefaultExceptionHandlerResponseModel()
        {
            StatusCode = System.Net.HttpStatusCode.InternalServerError,
            Detail = useExceptionsDetails
                       ? exception.ToString()
                       : ExceptionHandlingConstants.DefaultExceptionMessage
        };

        logger?.LogError(exception, exception.ToString());
        await context.WriteResponseAsync(res, res.StatusCode);
    }
}
