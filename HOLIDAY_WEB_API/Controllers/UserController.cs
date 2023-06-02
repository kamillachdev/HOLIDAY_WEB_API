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

            var cookieName = _configuration["CookieName"];
            var cookieExpires = _configuration["CookieExpires"];

            if (cookieName == null)
            {
                throw new Exception("Cookie name not found in configuration file.");
            }
            if (cookieExpires == null)
            {
                throw new Exception("Cookie expiraton time not found in configuration file.");
            }

            var success = double.TryParse(cookieExpires, out var parsedCookieExpires);
            if (!success)
            {
                throw new FormatException();
            }

            var userAgent = Request.Headers["User-Agent"].ToString();
            bool isSameSiteNoneCompatible = userAgent.Contains("Chrome") || userAgent.Contains("Firefox") || userAgent.Contains("Safari") || userAgent.Contains("Opera");

            var cookieSettings = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddMinutes(parsedCookieExpires),
                Secure = true,
                SameSite = isSameSiteNoneCompatible ? SameSiteMode.None : SameSiteMode.Lax
            };

            Response.Cookies.Append(cookieName, jwtToken, cookieSettings);

            return Ok(user);
        }
    }
}
