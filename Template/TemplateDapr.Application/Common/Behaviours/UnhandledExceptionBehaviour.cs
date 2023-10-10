using Application.Telemetry;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                string requestJson = JsonSerializer.Serialize(request);

                using var activity = ConsumerActivitySource.Source.StartActivity($"{requestName}");
                if (activity != null)
                {
                    activity!.SetStatus(System.Diagnostics.ActivityStatusCode.Error);
                    activity!.SetTag("request.json", requestJson);
                    activity!.SetTag("error.message", ex.Message);
                }
                _logger.LogError(ex, "Request: Unhandled Exception for Request {Name} {@requestJson}", requestName, requestJson);

                throw;
            }
        }
    }
}