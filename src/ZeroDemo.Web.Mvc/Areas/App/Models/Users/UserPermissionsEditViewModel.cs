using Abp.AutoMapper;
using ZeroDemo.Authorization.Users;
using ZeroDemo.Authorization.Users.Dto;
using ZeroDemo.Web.Areas.App.Models.Common;

namespace ZeroDemo.Web.Areas.App.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}