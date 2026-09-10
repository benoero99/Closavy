using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Closavy.Server.HealthCheck;

public class HealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var isHealthy = true; // Replace with your actual health check logic

        if (isHealthy)
        {
            return Task.FromResult(HealthCheckResult.Healthy("The application is healthy."));
        }

        return Task.FromResult(HealthCheckResult.Unhealthy("The application is unhealthy."));
    }
}