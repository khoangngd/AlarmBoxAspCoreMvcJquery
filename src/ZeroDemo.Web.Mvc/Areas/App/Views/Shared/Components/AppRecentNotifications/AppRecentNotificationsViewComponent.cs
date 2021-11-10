using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZeroDemo.Web.Areas.App.Models.Layout;
using ZeroDemo.Web.Views;

namespace ZeroDemo.Web.Areas.App.Views.Shared.Components.AppRecentNotifications
{
    public class AppRecentNotificationsViewComponent : ZeroDemoViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string cssClass)
        {
            var model = new RecentNotificationsViewModel
            {
                CssClass = cssClass
            };
            
            return Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}
