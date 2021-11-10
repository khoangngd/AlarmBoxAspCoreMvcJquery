using Abp.Application.Services;
using Abp.Application.Services.Dto;
using ZeroDemo.Authorization.Permissions.Dto;

namespace ZeroDemo.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
