using HOLIDAY_WEB_API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HOLIDAY_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IAuthorizationUser _authorizationUser;
        private readonly IConfiguration _configuration;

        public UserController(IUserServices userServices, IAuthorizationUser authorizationUser, IConfiguration configuration)
        {
            _userServices = userServices;
            _authorizationUser = authorizationUser;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("getUser")]
        public IActionResult GetUser(string email, string password)
        {
            var user = _userServices.GetUserByEmail(email, password);
            if (user == null)
            {
                return NotFound();
            }

            // Generate JWT token
            string userId = user.Id.ToString();
            string username = user.UserName;
            string userEmail = user.Email;
            string userRole = user.Role;

            string jwtToken = _authorizationUser.CreateJwt(userId, username, userEmail, userRole);

            Response.Cookies.Append("test", "test");

            // Add JWT token as a cookie
            Response.Cookies.Append(_configuration["CookieName"], jwtToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddMinutes(double.Parse(_configuration["CookieExpires"])),
                SameSite = SameSiteMode.None
            });

            return Ok(user);
        }
    }
}
