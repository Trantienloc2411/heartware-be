using System;
using Net.payOS;
using Net.payOS.Types;
using Service.IService;
using Service.viewModels;

namespace Service.Services;

public class Payment : IPayment
{
    private readonly PayOS _payOs;

    public Payment(PayOS payOs)
    {
        _payOs = payOs;
    }
    public async Task<string> CreatePaymentLink(PaymentRequestLinkViewModel viewModel)
        {
        try
        {
            int orderId = int.Parse(viewModel.orderId);

            List<ItemData> items = new List<ItemData>();
            

            PaymentData paymentData = new PaymentData(Convert.ToInt32(orderId),
                viewModel.priceTotal, $"{orderId}", items, viewModel.cancelUrl, viewModel.returnUrl); //return cancelUrl and successUrl
            CreatePaymentResult createPaymentResult = await _payOs.createPaymentLink(paymentData);
            //return url of payment
            return createPaymentResult.checkoutUrl;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<PaymentLinkInformation> GetOrderPayment(string orderId)
    {
        try
        {
            int orderCode = int.Parse(orderId);
            PaymentLinkInformation paymentLinkInformation = await _payOs.getPaymentLinkInformation(orderCode);
            return paymentLinkInformation;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<PaymentLinkInformation> CancelOrder(string orderId)
    {
        try
        {
            int orderCode = int.Parse(orderId);
            PaymentLinkInformation paymentLinkInformation = await _payOs.cancelPaymentLink(orderCode);
            return paymentLinkInformation;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
