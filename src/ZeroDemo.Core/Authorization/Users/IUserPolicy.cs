using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace ZeroDemo.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
