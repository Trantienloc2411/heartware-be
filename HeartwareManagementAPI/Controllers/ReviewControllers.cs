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
        
        public ReviewControllers(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //[HttpPost]
        //public IActionResult Post(Guid productId,[FromBody])
        //{

        //}

    }
}
