using Abp.AutoMapper;
using ZeroDemo.Authorization.Roles.Dto;
using ZeroDemo.Web.Areas.App.Models.Common;

namespace ZeroDemo.Web.Areas.App.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode => Role.Id.HasValue;
    }
}