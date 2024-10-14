using System;

namespace Service.viewModels;

public class PaymentRequestLinkViewModel
{
    public string? orderId { get; set; }
    public string? description { get; set; }
    public int priceTotal { get; set; }
    public string returnUrl { get; set; }  = "http://heartware.vercel.app/order/complete";
    public string cancelUrl { get; set; } = "http://heartware.vercel.app/order/cancel";
    
}

public class ItemShowRequest
{
    public string productName { get; set; }
    public int quantity { get; set; }
    public double priceSingle { get; set; }
}
