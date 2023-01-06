using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Common.ExceptionHandlingExtension.Infrastructre.Models.OptionModels;

public class ExceptionHandlingOptions
{
    internal Dictionary<Type, Func<HttpContext, Exception, ILogger, Task>> ExceptionHandlersDictionary;
    internal Func<HttpContext, Exception, ILogger, Task> ExceptionHandler;
    internal ILogger Logger { get; set; }

    public ExceptionHandlingOptions()
    {
        ExceptionHandlersDictionary = new Dictionary<Type, Func<HttpContext, Exception, ILogger, Task>>();
    }

    public bool LoggingEnabled { get; internal set; } = false;
    public bool UseExceptionDetails { get; set; }

    public void UseLogger<T>(T logger) where T : ILogger
    {
        Logger = logger;
        UseLogger();
    }

    public void UseLogger()
    {
        LoggingEnabled = true;
    }

    public void UseCustomHandler(Func<HttpContext, Exception, ILogger, Task> exceptionHandler)
    {
        ExceptionHandler = exceptionHandler;
    }

    public void AddCustomHandler<TException>(Func<HttpContext, Exception, ILogger, Task> handler) where TException : Exception
    {
        var added = ExceptionHandlersDictionary.TryAdd(typeof(TException), handler);

        if (!added)
            throw new InvalidOperationException($"{typeof(TException)} is already added for another handler");
    }
}
