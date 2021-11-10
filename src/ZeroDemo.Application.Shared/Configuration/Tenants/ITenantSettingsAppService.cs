using System.Threading.Tasks;
using Abp.Application.Services;
using ZeroDemo.Configuration.Tenants.Dto;

namespace ZeroDemo.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
