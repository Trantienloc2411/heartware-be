using AutoMapper;
using BusinessObjects.Entities;
using BusinessObjects.HeartwareENUM;
using HeartwareManagementAPI.DTOs.ProductDTO;
using HeartwareManagementAPI.DTOs.ReviewDTOs;
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
    public async Task<IEnumerable<ProductDTO>> GetAllProducts()
    {
        var orderDetails = _unitOfWork.OrderDetailRepository.GetAll();
        var products = await _unitOfWork.ProductRepository.GetAllWithIncludeAsync(p => true,
            p => p.Reviews,
            p => p.Category,
            pd=>pd.ProductDetails);

        var productDTOs = _mapper.Map<IEnumerable<ProductDTO>>(products);
        foreach (var product in productDTOs)
        {
            product.ItemBought = orderDetails.Where(c => c.ProductId == product.ProductId).Count();
        }
        return productDTOs;
    }
    
    [HttpGet("id")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.GetSingleWithIncludeAsync(t => t.ProductId == id,
            t => t.Reviews,
            c => c.Category,
            pd=> pd.ProductDetails);

        var result = _mapper.Map<ProductDTO>(product);

        return Ok(result);
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] AddProductDTO addProductDto)
    {
        var trimmedImageUrl = string.IsNullOrEmpty(addProductDto.ImageUrl) 
            ? null 
            : addProductDto.ImageUrl.Trim(' ', ',');
        var newProduct = new Product
        {
            ProductId = Guid.NewGuid(),
            ProductName = addProductDto.ProductName,
            Description = addProductDto.Description,
            Price = addProductDto.Price,
            UnitsInStock = addProductDto.UnitsInStock,
            CategoryId = addProductDto.CategoryId,
            ImageUrl = trimmedImageUrl,
            ProductStatus= (int)addProductDto.ProductStatus,
            CreatedDate = DateTime.Now,
        };
        _unitOfWork.ProductRepository.Insert(newProduct);

        // Add ProductDetails
        if (addProductDto.ProductDetails != null && addProductDto.ProductDetails.Any())
        {
            foreach (var detail in addProductDto.ProductDetails)
            {
                var newProductDetail = new ProductDetail
                {
                    ProductParam = detail.ProductParam,
                    ProductValue = detail.ProductValue,
                    ProductId = newProduct.ProductId,
                };
                _unitOfWork.ProductDetailRepository.Insert(newProductDetail);
            }
        }


        _unitOfWork.Save();
        return Ok();
    }

    [HttpPut]
    public IActionResult UpdateProduct([FromBody] UpdateProductDTO updateProductDto)
    {
        var existingProduct= _unitOfWork.ProductRepository.GetByID(updateProductDto.ProductId);
        if (existingProduct == null)
        {
            return BadRequest($"Product with id {updateProductDto.ProductId} does not exist");
        }
        
        var createdDate = existingProduct.CreatedDate;
        _mapper.Map(updateProductDto, existingProduct);
        
        existingProduct.CreatedDate = createdDate;
        existingProduct.UpdatedDate = DateTime.UtcNow;

        if (updateProductDto.ProductDetails != null && updateProductDto.ProductDetails.Any())
        {
            foreach (var updatedDetailDto in updateProductDto.ProductDetails)
            {
                var existingProductDetail = existingProduct.ProductDetails
                    .FirstOrDefault(detail => detail.ProductDetailId == updatedDetailDto.ProductDetailId);

                if (existingProductDetail != null)
                {
                    _mapper.Map(updatedDetailDto, existingProductDetail);
                }
                else
                {
                    return BadRequest($"Product detail with id {updatedDetailDto.ProductDetailId} does not exist");
                }
            }
        }

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