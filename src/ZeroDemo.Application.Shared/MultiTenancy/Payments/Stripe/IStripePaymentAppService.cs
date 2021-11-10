using System.Threading.Tasks;
using Abp.Application.Services;
using ZeroDemo.MultiTenancy.Payments.Dto;
using ZeroDemo.MultiTenancy.Payments.Stripe.Dto;

namespace ZeroDemo.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}