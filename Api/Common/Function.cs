using BlazorApp.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp.Api.Common;

public abstract class Function : IFunctionExceptionFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<Function> _logger;

    protected Function(IHttpContextAccessor httpContextAccessor, ILogger<Function> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public Task OnExceptionAsync(FunctionExceptionContext exceptionContext, CancellationToken cancellationToken)
    {
        _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        _httpContextAccessor.HttpContext.Response.ContentType = "application/json";

        _logger.LogError($"Something went wrong: {exceptionContext.Exception}");

        return _httpContextAccessor.HttpContext.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = _httpContextAccessor.HttpContext.Response.StatusCode,
            Message = "Internal Server Error"
        }.ToString(), cancellationToken);
    }
}