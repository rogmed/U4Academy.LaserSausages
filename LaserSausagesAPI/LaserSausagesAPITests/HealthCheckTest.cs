using LaserSausagesAPI.Controllers;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LaserSausagesAPITests {
    [TestClass]
    public class HealthCheckTest {
        [TestMethod]
        public async Task HealthCheck_ShouldReturnHealthy() {
            HealthCheck healthCheck = new HealthCheck();

            var result = await healthCheck.CheckHealthAsync(new HealthCheckContext());
            Assert.AreEqual(HealthStatus.Healthy, result.Status);
        }
    }
}
