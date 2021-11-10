using Abp.Authorization;
using ZeroDemo.Authorization.Roles;
using ZeroDemo.Authorization.Users;

namespace ZeroDemo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
