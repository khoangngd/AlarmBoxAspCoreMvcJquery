using System.Threading.Tasks;
using Abp.Application.Services;

namespace ZeroDemo.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
