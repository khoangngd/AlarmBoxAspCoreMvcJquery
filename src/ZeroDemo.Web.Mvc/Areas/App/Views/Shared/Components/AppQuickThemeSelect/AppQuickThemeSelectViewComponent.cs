using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZeroDemo.Web.Areas.App.Models.Layout;
using ZeroDemo.Web.Views;

namespace ZeroDemo.Web.Areas.App.Views.Shared.Components.
    AppQuickThemeSelect
{
    public class AppQuickThemeSelectViewComponent : ZeroDemoViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string cssClass)
        {
            return Task.FromResult<IViewComponentResult>(View(new QuickThemeSelectionViewModel
            {
                CssClass = cssClass
            }));
        }
    }
}
