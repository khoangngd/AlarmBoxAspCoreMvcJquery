using System.Threading.Tasks;
using Abp.Application.Services;
using ZeroDemo.Sessions.Dto;

namespace ZeroDemo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
