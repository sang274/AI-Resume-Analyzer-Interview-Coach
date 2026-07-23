using AIResumeAnalyzer.Application.Interfaces.IAuthenticateService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AIResumeAnalyzer.Application.Common.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger,
            ICurrentUserService currentUserService,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            var stopwatch = Stopwatch.StartNew();

            var correlationId = _httpContextAccessor.HttpContext?.TraceIdentifier ?? Guid.NewGuid().ToString();

            var userId = _currentUserService.UserId?.ToString() ?? "Anonymous";

            using (_logger.BeginScope(new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId,
                ["UserId"] = userId
            }))
            {
                _logger.LogInformation("Handling {RequestName}. Payload: {Payload}", requestName, JsonSerializer.Serialize(PayloadSanitizer.Sanitize(PayloadSanitizer.Sanitize(request))));

                try
                {
                    var response = await next();

                    stopwatch.Stop();

                    if (stopwatch.ElapsedMilliseconds >
                        LoggingConstants.SlowRequestThreshold)
                    {
                        _logger.LogWarning("{RequestName} is slow. Execution Time: {Elapsed} ms", requestName, stopwatch.ElapsedMilliseconds);
                    }

                    _logger.LogInformation("Completed {RequestName} in {Elapsed} ms", requestName, stopwatch.ElapsedMilliseconds);

                    return response;
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();

                    _logger.LogError(ex, "Failed {RequestName} after {Elapsed} ms", requestName, stopwatch.ElapsedMilliseconds);

                    throw;
                }
            }
        }
    }
}
