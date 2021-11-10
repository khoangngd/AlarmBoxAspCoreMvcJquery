using Abp.Application.Services;
using ZeroDemo.Dto;
using ZeroDemo.Logging.Dto;

namespace ZeroDemo.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
