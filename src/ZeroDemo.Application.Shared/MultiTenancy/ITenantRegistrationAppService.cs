using System.Threading.Tasks;
using Abp.Application.Services;
using ZeroDemo.Editions.Dto;
using ZeroDemo.MultiTenancy.Dto;

namespace ZeroDemo.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}