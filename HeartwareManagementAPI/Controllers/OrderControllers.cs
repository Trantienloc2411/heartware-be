using AutoMapper;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.Implement;

namespace HeartwareManagementAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderControllers : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderControllers(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<Order>> Get()
    {
        return await _unitOfWork.OrderRepository.GetAllWithIncludeAsync(t => true, 
            null);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> Get(Guid id)
    {
        return await _unitOfWork.OrderRepository.GetSingleWithIncludeAsync(t => t.OrderId == id, 
            t => t.OrderDetails, t => t.Discount, t => t.Shippings);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> Post(Order order)
    {
        
    }
    
    
    
    
    
}