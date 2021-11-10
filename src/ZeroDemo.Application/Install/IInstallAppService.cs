using System.Threading.Tasks;
using Abp.Application.Services;
using ZeroDemo.Install.Dto;

namespace ZeroDemo.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}