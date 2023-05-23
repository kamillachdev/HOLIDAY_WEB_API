using HOLIDAY_WEB_API.Models;
using HOLIDAY_WEB_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HOLIDAY_WEB_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthDL _authDL;
        public AuthController(IAuthDL authDL)
        {
            _authDL = authDL;
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            try
            {
                response = await _authDL.SignUp(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            try
            {
                response = await _authDL.SignIn(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
