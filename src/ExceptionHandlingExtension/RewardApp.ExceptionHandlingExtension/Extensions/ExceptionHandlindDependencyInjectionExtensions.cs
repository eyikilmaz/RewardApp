using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RewardApp.Common.ExceptionHandlingExtension.Infrastructre.Models.OptionModels;
using RewardApp.ExceptionHandlingExtension.Infrastructre.ExceptionHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.ExceptionHandlingExtension.Extensions;

public static class ExceptionHandlindDependencyInjectionExtensions
{
    public static Task ConfigureExceptionHandling(this IApplicationBuilder app)
    {
        return app.ConfigureExceptionHandling(options =>
        {
            options.UseLogger();
        });
    }

    public static Task ConfigureExceptionHandling(this IApplicationBuilder app, Action<ExceptionHandlingOptions> optionsAction)
    {
        ExceptionHandlingOptions opt = new();
        optionsAction(opt);

        return ConfigureExceptionHandling(app, opt);
    }

    public static async Task ConfigureExceptionHandling(this IApplicationBuilder app,
        ExceptionHandlingOptions opt)
    {
        ILogger logger = opt.Logger;

        if(logger is null && opt.LoggingEnabled)
        {
            logger = GetLoggerService(app.ApplicationServices);
        }

        app.UseExceptionHandler(async options =>
        {
            if (opt.ExceptionHandler is not null)
            {
                await RegisterHandlers(options,
                                       logger,
                                       opt.ExceptionHandler,
                                       opt);
            }
            else
            {
                await RegisterHandlers(options,
                                      logger,
                                       (c, e, l) => DefaultExceptionHandler.Handle(c, e, l, opt.UseExceptionDetails),
                                      opt);
            }
        });

        await Task.CompletedTask;
    }

    private static ILogger GetLoggerService(IServiceProvider sp)
    {
        // Try to get ILogger first
        var serviceLogger = sp.GetService<ILogger>();

        if (serviceLogger is not null)
            return serviceLogger;

        // If no ILogger, try to create yours by using ILoggerFactory
        var logFactory = sp.GetService<ILoggerFactory>();
        serviceLogger = logFactory.CreateLogger<ExceptionHandlingOptions>();

        return serviceLogger;
    }

    private static async Task RegisterHandlers(IApplicationBuilder app,
                                               ILogger logger,
                                               Func<HttpContext, Exception, ILogger, Task> exceptionHandler,
                                                ExceptionHandlingOptions options)
    {
        app.Run(async context =>
        {
            var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
            var type = exceptionObject.Error.GetType();
            options.ExceptionHandlersDictionary.TryGetValue(type, out var handler);

            if (handler is not null)
            {
                await handler.Invoke(context, exceptionObject.Error, logger);
            }
            else
            {
                await exceptionHandler.Invoke(context, exceptionObject.Error, logger);
            }

            await Task.CompletedTask;
        });
    }
}
