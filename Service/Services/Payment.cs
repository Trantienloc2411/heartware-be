using System;
using Net.payOS;
using Net.payOS.Types;
using Repository.Implement;
using Service.IService;
using Service.viewModels;

namespace Service.Services;

public class Payment : IPayment
{
    private readonly PayOS _payOs;
    private readonly IUnitOfWork _unitOfWork;

    public Payment(PayOS payOs, IUnitOfWork unitOfWork)
    {
        _payOs = payOs;
        _unitOfWork = unitOfWork;   
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

    public async Task<String> GetPaymentInformation(WebhookType webhookBody)
    {
        WebhookData verifiedData = _payOs.verifyPaymentWebhookData(webhookBody);
        
        string responseCode = verifiedData.code;
        string orderCode = verifiedData.orderCode.ToString();

        var order = _unitOfWork.OrderRepository
            .GetAll()
            .Where(c => c.OrderStatus == 1)
            .Select(o => new
            {
                Order = o,
                NumericId = string.Concat(o.OrderId.ToString().Where(char.IsDigit)).Substring(0, 8)
            })
            .FirstOrDefault(x => x.NumericId == orderCode);

        if (order != null)
        {
            if (responseCode == "00")
            {
                order.Order.OrderStatus = 2;
                _unitOfWork.Save();
            }

            // Sử dụng matchedOrder và numericId ở đây
        }
        return responseCode;
    }
}
