using HOLIDAY_WEB_API.Interfaces;
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
        [Route("getUser")]
        public IActionResult GetUser(string email, string password) 
        {
            var user = _userServices.GetUserByEmail(email, password);
            if(user == null) 
            {
                return NotFound();
            }
            return Ok(user);
        }

    }
}
