using System.Collections.Generic;
using ZeroDemo.Editions;
using ZeroDemo.Editions.Dto;
using ZeroDemo.MultiTenancy.Payments;
using ZeroDemo.MultiTenancy.Payments.Dto;

namespace ZeroDemo.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
