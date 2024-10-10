using AutoMapper;
using BusinessObjects.Entities;
using HeartwareManagementAPI.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Mvc;
using Repository.Implement;

namespace HeartwareManagementAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IMapper mapper, IUnitOfWork unitOfWork) {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IEnumerable<Category> GetAllCategories()
    {
        return  _unitOfWork.CategoryRepository.GetAll();    
    }

    [HttpPost]
    public IActionResult CreateCategory([FromBody] CategoryDTO categoryDto)
    {
        var newCategory = new Category
        {
            CategoryName = categoryDto.CategoryName,
        };
        _unitOfWork.CategoryRepository.Insert(newCategory);
        _unitOfWork.Save();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, [FromBody] CategoryDTO categoryDto)
    {
        var existingCategory = _unitOfWork.CategoryRepository.GetByID(id);
        if (existingCategory == null)
        {
            return BadRequest($"Category with id {id} does not exist");
        }
        
        _mapper.Map(categoryDto, existingCategory);
        
        _unitOfWork.CategoryRepository.Update(existingCategory);
        _unitOfWork.Save();
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var existingCategory = _unitOfWork.CategoryRepository.GetByID(id);
        if (existingCategory == null)
        {
            return BadRequest($"Category with id {id} does not exist");
        }
        _unitOfWork.CategoryRepository.Delete(existingCategory);
        _unitOfWork.Save();
        return Ok();
    }
}