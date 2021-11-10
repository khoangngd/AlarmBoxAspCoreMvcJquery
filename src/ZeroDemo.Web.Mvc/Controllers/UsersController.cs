using Abp.AspNetCore.Mvc.Authorization;
using ZeroDemo.Authorization;
using ZeroDemo.Storage;
using Abp.BackgroundJobs;
using Abp.Authorization;

namespace ZeroDemo.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}