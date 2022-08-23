using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LaserSausagesAPI.Controllers {
    public class HealthCheck : IHealthCheck {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default) {
            return Task.FromResult(HealthCheckResult.Healthy("App service is healthy"));
        }
    }
}
