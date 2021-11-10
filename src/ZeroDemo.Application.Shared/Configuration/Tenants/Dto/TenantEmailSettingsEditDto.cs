using Abp.Auditing;
using ZeroDemo.Configuration.Dto;

namespace ZeroDemo.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}