using Abp.AutoMapper;
using ZeroDemo.MultiTenancy.Dto;

namespace ZeroDemo.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
