using ZeroDemo.Editions;
using ZeroDemo.Editions.Dto;
using ZeroDemo.MultiTenancy.Payments;
using ZeroDemo.Security;
using ZeroDemo.MultiTenancy.Payments.Dto;

namespace ZeroDemo.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
