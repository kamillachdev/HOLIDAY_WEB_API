using HOLIDAY_WEB_API.Interfaces;
using HOLIDAY_WEB_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HOLIDAY_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IAuthorizationUser _authorizationUser;
        public UserController(IUserServices userServices, IAuthorizationUser authorizationUser)
        {
            _userServices = userServices;
            _authorizationUser = authorizationUser;
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

            // Generate JWT token
            string userId = user.Id.ToString();
            string username = user.UserName;
            string userEmail = user.Email;
            string userRole = user.Role;

            string jwtToken = _authorizationUser.CreateJwt(userId, username, userEmail, userRole);

            // Add JWT token as a cookie
            _authorizationUser.AddJwtCookie(jwtToken);


            return Ok(user);
        }

    }
}
