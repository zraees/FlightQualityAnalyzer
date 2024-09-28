// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FlightQualityAnalyzer.API.Middlewares;

using System.Threading;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

/// <summary>
/// IExceptionHandler introduce from .Net core 8 and it's provide robust exceptional handling.
/// here I'm just logging the error and then write problemDetail in response.
/// I used problemDetail to provide standard format for error responses.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handle the error and return true upon successfully catching the error.
    /// Use ValueTask, which is more efficient than Task because it is like a struct and managed/allocated on the stack.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="exception"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Server error"
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);

        return true;
    }
}
