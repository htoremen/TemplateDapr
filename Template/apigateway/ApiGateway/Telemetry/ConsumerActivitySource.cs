using System.Diagnostics;

namespace Apigateway.Telemetry;

public static class ConsumerActivitySource
{
    public static readonly ActivitySource Source = OpenTelemetryExtensions.CreateActivitySource();
}

