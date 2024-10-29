using AutoMapper;
using BusinessObjects.Entities;
using HeartwareManagementAPI.DTOs.ReviewDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Implement;

namespace HeartwareManagementAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReviewControllers : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper  _mapper;
        public ReviewControllers(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        [HttpPost]
        public IActionResult Post([FromBody] PostReviewDTO reviewDTO)
        {
            var review = _mapper.Map<Review>(reviewDTO);

            _unitOfWork.ReviewRepository.Insert(review);


            _unitOfWork.Save();

            return CreatedAtAction(nameof(Post), new { id = review.ReviewId }, review);
        }


    }
}
