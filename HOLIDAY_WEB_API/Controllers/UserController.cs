using HOLIDAY_WEB_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HOLIDAY_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpGet]
        [Route("allUsers")]
        public IActionResult GetAllUsers() {
            var result = _userServices.GetAllUsers();
            return Ok(result);
        }
    }
}
