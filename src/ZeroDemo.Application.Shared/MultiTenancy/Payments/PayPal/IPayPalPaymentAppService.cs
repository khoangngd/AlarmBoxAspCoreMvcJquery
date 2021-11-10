using System.Threading.Tasks;
using Abp.Application.Services;
using ZeroDemo.MultiTenancy.Payments.PayPal.Dto;

namespace ZeroDemo.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
