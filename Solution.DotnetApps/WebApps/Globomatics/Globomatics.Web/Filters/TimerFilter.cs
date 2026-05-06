using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Globomatics.Web.Filters;

public class TimerFilter : IAsyncActionFilter
{
    private readonly ILogger<TimerFilter> _logger;
    private Stopwatch Stopwatch { get; set; } = default!;

    public TimerFilter(ILogger<TimerFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        Stopwatch = new Stopwatch();
        Stopwatch.Start();
        _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} started.");
        await next();
        Stopwatch.Stop();
        _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} completed.");
        _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} ran for {Stopwatch.ElapsedMilliseconds} MilliSeconds.");

    }
}
