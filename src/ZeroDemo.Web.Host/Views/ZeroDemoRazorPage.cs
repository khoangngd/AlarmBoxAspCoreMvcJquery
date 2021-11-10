using Abp.AspNetCore.Mvc.Views;

namespace ZeroDemo.Web.Views
{
    public abstract class ZeroDemoRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected ZeroDemoRazorPage()
        {
            LocalizationSourceName = ZeroDemoConsts.LocalizationSourceName;
        }
    }
}
