using System.Text.Json;
using System.Text.RegularExpressions;
using System.Transactions;
using AutoMapper;
using BusinessObjects.Entities;
using HeartwareManagementAPI.DTOs.Order;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Repository.Implement;
using Service.IService;
using Service.Services;
using Service.viewModels;

namespace HeartwareManagementAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderControllers : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    private readonly IConfiguration _configuration;

    private readonly IPayment _payment;

    private readonly ILogger<OrderControllers> _logger;
    

    public OrderControllers(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, IPayment payment, ILogger<OrderControllers> logger)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _mapper = mapper;
        _payment = payment;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<Order>> Get()
    {
        var list = await _unitOfWork.OrderRepository.GetAllWithIncludeAsync(t => true,
            t => t.Discount);

        return list;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var order = await _unitOfWork.OrderRepository.GetSingleWithIncludeAsync(t => t.OrderId == id,
            t => t.OrderDetails, t => t.Discount);

        var result = _mapper.Map<GetOrderById>(order);
        
        

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> Post(AddOrder order)
    {

        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                Order o = new Order();

                OrderDetail od;

                o.OrderId = Guid.NewGuid();
                o.UserId = null;
                o.OrderDate = DateTime.Now;
                o.ConfirmDate = null;
                o.OrderStatus = 1;
                o.PaymentMethod = order.PaymentMethod;
                o.DiscountId = order.DiscountId;
                o.TotalAmount = order.TotalAmount;
                o.FirstName = order.FirstName;
                o.LastName = order.LastName;
                o.Email = order.Email;
                o.Phone = order.Phone;
                o.Address = order.Address;

                _unitOfWork.OrderRepository.Insert(o);
                _unitOfWork.Save();


                foreach (var item in order.OrderDetails)
                {
                    od = new OrderDetail();
                    od.OrderDetailId = Guid.NewGuid();
                    od.OrderId = o.OrderId;
                    od.ProductId = item.ProductId;
                    od.Quantity = item.Quantity;
                    od.Price = item.Price;
                    od.Subtotal = item.Subtotal;
                    _unitOfWork.OrderDetailRepository.Insert(od);
                    _unitOfWork.Save();
                }

                transaction.Complete();
                string fullname = o.FirstName + o.LastName;
                SentMail.SendMail(
                    _configuration,
                    o.Email,
                    fullname,
                    o.OrderId.ToString(),
                    DateTime.Now,
                    (decimal)o.TotalAmount
                );

                if(order.PaymentMethod == 1)
                {
                    string orderId = Regex.Replace(o.OrderId.ToString(), @"\D", "");

                    if(orderId.Length > 10) {
                        orderId = orderId.Substring(0,8);
                    }

                    PaymentRequestLinkViewModel pay = new PaymentRequestLinkViewModel();
                    pay.orderId = orderId;
                    pay.description = "Order for id " + orderId;
                    pay.priceTotal =  Convert.ToInt32(o.TotalAmount);
                    string paymentlink = await _payment.CreatePaymentLink(pay);
                    return Ok(paymentlink);
                }
                else{
                    
                }




                return Ok();
            }
            catch (System.Exception ex)
            {
                transaction.Dispose();
                return BadRequest(ex.Message);
            }
        }
    }

    /// <summary>
    /// Update order by passing new field if the field is not update or no new data, please
    /// passing with old data
    /// </summary>
    /// <param name="order">Include - OrderId, UserId (origin is null), ConfirmDate, OrderStatus</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Put(UpdateOrder order)
    {
        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                var o = _unitOfWork.OrderRepository.GetByID(order.Id);
                if (o == null) return NotFound("Can't find this order");
                o.UserId = order.UserId;
                o.ConfirmDate = order.ConfirmDate;
                o.OrderStatus = order.OrderStatus;


                _unitOfWork.OrderRepository.Update(o);
                _unitOfWork.Save();

                transaction.Complete();
                return NoContent();
            }
            catch (System.Exception ex)
            {
                transaction.Dispose();
                return BadRequest(ex);
            }
        }
    }

    [HttpPost("GetPaymentLinkInformation")]
    public async Task<IActionResult> GetPaymentLinkInformation([FromBody] WebhookType webhookType)
    {
        try
        {
            _logger.LogInformation($"Requesting payment information for ID: {webhookType.data.orderCode}");
            var result = await _payment.GetPaymentInformation(webhookType);
            _logger.LogInformation($"Received payment information: {JsonSerializer.Serialize(result)}");
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error getting payment information: {ex.Message}");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }




}