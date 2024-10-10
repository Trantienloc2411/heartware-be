using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.HeartwareENUM;
using HeartwareManagementAPI.DTOs.ProductDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using Repository.Implement;

namespace HeartwareManagementAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IMapper mapper, IUnitOfWork unitOfWork) {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products = _unitOfWork.ProductRepository.ExecuteStoredProcedure<ProductDTOs>("GetAllProducts");
        return Ok(products);
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] AddProductDTO addProductDto)
    {
        var newProduct = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = addProductDto.ProductName,
            Description = addProductDto.Description,
            Price = addProductDto.Price,
            UnitsInStock = addProductDto.UnitsInStock,
            CategoryId = addProductDto.CategoryId,
            ImageUrl = addProductDto.ImageUrl,
            ProductStatus= (int)addProductDto.ProductStatus,
            CreatedDate = DateTime.UtcNow,
        };
        _unitOfWork.ProductRepository.Insert(newProduct);
        _unitOfWork.Save();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(Guid id, [FromBody] AddProductDTO addProductDto)
    {
        var existingProduct= _unitOfWork.ProductRepository.GetByID(id);
        if (existingProduct == null)
        {
            return BadRequest($"Product with id {id} does not exist");
        }
        
        var createdDate = existingProduct.CreatedDate;
        _mapper.Map(addProductDto, existingProduct);
        
        existingProduct.CreatedDate = createdDate;
        existingProduct.UpdatedDate = DateTime.UtcNow;
        
        
        _unitOfWork.ProductRepository.Update(existingProduct);
        _unitOfWork.Save();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(Guid id)
    {
        var existingProduct = _unitOfWork.ProductRepository.GetByID(id);
        if (existingProduct == null)
        {
            return BadRequest($"Product with id {id} does not exist");
        }
        _unitOfWork.ProductRepository.Delete(existingProduct);
        _unitOfWork.Save();
        return Ok();
    }
}