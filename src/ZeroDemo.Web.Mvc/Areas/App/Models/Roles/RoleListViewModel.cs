using System.Collections.Generic;
using Abp.Application.Services.Dto;
using ZeroDemo.Authorization.Permissions.Dto;
using ZeroDemo.Web.Areas.App.Models.Common;

namespace ZeroDemo.Web.Areas.App.Models.Roles
{
    public class RoleListViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}