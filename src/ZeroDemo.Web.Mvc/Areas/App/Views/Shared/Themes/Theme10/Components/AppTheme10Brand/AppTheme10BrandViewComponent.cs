using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZeroDemo.Web.Areas.App.Models.Layout;
using ZeroDemo.Web.Session;
using ZeroDemo.Web.Views;

namespace ZeroDemo.Web.Areas.App.Views.Shared.Themes.Theme10.Components.AppTheme10Brand
{
    public class AppTheme10BrandViewComponent : ZeroDemoViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppTheme10BrandViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headerModel = new HeaderViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(headerModel);
        }
    }
}
