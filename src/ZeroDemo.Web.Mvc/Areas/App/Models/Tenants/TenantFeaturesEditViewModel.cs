using Abp.AutoMapper;
using ZeroDemo.MultiTenancy;
using ZeroDemo.MultiTenancy.Dto;
using ZeroDemo.Web.Areas.App.Models.Common;

namespace ZeroDemo.Web.Areas.App.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}