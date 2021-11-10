using System.Collections.Generic;
using ZeroDemo.Editions.Dto;
using ZeroDemo.MultiTenancy.Payments;

namespace ZeroDemo.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}