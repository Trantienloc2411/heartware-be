using System;
using Net.payOS.Types;
using Service.viewModels;

namespace Service.IService;

public interface IPayment
{
    Task<string> CreatePaymentLink(PaymentRequestLinkViewModel viewModel);
    Task<PaymentLinkInformation> GetOrderPayment(string orderId);

    Task<PaymentLinkInformation> CancelOrder(string orderId);

}
