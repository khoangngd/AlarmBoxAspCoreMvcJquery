using System.Collections.Generic;
using MvvmHelpers;
using ZeroDemo.Models.NavigationMenu;

namespace ZeroDemo.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}