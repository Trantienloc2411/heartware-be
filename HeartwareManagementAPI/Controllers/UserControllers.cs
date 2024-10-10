using AutoMapper;
using BusinessObjects.Entities;
using HeartwareManagementAPI.DTOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Implement;

namespace HeartwareManagementAPI.Controllers
{


    [Route("[controller]")]
    [ApiController]
    public class UserControllers : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private User userMapper = new User();
        

        public UserControllers(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<User> Get(Guid id)
        {
            return await _unitOfWork.UserRepository.GetSingleWithIncludeAsync(t => t.UserId == id, t => t.Role);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AddUser user)
        {
            try
            {
                userMapper = _mapper.Map(user, userMapper);
                _unitOfWork.UserRepository.Insert(userMapper);
                return Ok(userMapper);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

}
}
