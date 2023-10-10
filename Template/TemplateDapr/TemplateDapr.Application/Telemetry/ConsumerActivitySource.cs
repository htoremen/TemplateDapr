using System.Diagnostics;

namespace Application.Telemetry
{
    public static class ConsumerActivitySource
    {
        public static readonly ActivitySource Source = OpenTelemetryExtensions.CreateActivitySource();
    }
}