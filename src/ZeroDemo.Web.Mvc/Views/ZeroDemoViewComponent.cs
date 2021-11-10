using Abp.AspNetCore.Mvc.ViewComponents;

namespace ZeroDemo.Web.Views
{
    public abstract class ZeroDemoViewComponent : AbpViewComponent
    {
        protected ZeroDemoViewComponent()
        {
            LocalizationSourceName = ZeroDemoConsts.LocalizationSourceName;
        }
    }
}