using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ZeroDemo.EntityFrameworkCore;

namespace ZeroDemo.HealthChecks
{
    public class ZeroDemoDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public ZeroDemoDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("ZeroDemoDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("ZeroDemoDbContext could not connect to database"));
        }
    }
}
