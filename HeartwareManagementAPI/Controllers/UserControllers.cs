using AutoMapper;
using BusinessObjects.Entities;
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

        public UserControllers(IMapper mapper, IUnitOfWork unitOfWork) {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return  _unitOfWork.UserRepository.GetAll(); 
        }

        

    }
}
