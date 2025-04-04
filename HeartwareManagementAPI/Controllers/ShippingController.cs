using AutoMapper;
using BusinessObjects.Entities;
using HeartwareManagementAPI.DTOs.ShippingDTOs;
using Microsoft.AspNetCore.Mvc;
using Repository.Implement;

namespace HeartwareManagementAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ShippingController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ShippingController(IMapper mapper, IUnitOfWork unitOfWork) {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IEnumerable<Shipping>> GetShippings()
    {
        var shippings = await _unitOfWork.ShippingRepository.GetAllWithIncludeAsync(s => true,
            s => s.Order);
        return shippings;
    }

    [HttpPost]
    public IActionResult PostShipping([FromBody] AddShippingDTO shipping)
    {
        var newShipping = new Shipping
        {
            ShippingId =Guid.NewGuid(),
            OrderId = shipping.OrderId,
            ShippingAddress = shipping.Order.Address,
            StartShipDate = shipping.StartShipDate,
            EndShipDate = shipping.EndShipDate,
            CancelDate = shipping.CancelDate,
            TrackingNumber = Guid.NewGuid().ToString().Replace("-" , "").ToUpper().Substring(0 , 15)
        };
        _unitOfWork.ShippingRepository.Insert(newShipping);
         _unitOfWork.Save();
         return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateShipping(Guid id,[FromBody] UpdateShippingDTO shipping )
    {
        var existingShipping = _unitOfWork.ShippingRepository.GetByID(id);
        if (existingShipping == null)
        {
            return BadRequest("Shipping not found");
        }

        existingShipping.ShippingAddress = shipping.ShippingAddress;
        existingShipping.StartShipDate = shipping.StartShipDate;
        existingShipping.EndShipDate = shipping.EndShipDate;
        existingShipping.CancelDate = shipping.CancelDate;
        
        _unitOfWork.ShippingRepository.Update(existingShipping);
        _unitOfWork.Save();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteShipping(Guid id)
    {
        var existingShipping = _unitOfWork.ShippingRepository.GetByID(id);
        if (existingShipping == null)
        {
            return BadRequest("Shipping not found");    
        }
        
        _unitOfWork.ShippingRepository.Delete(existingShipping);
        _unitOfWork.Save();
        return Ok();
    }
}