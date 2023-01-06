using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RewardApp.ExceptionHandlingExtension.Infrastructre;

internal class ExceptionHandlingConstants
{
    internal const string DefaultExceptionMessage = "Internal server error occured!";

    internal const string DefaultResponseContentType = "application/json";

    internal static JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };
}
