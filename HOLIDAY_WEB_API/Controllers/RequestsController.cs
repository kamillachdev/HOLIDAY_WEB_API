using HOLIDAY_WEB_API.Interfaces;
using HOLIDAY_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HOLIDAY_WEB_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestServices _requestServices;

        public RequestsController(IRequestServices requestServices)
        {
            _requestServices = requestServices;
        }

        [HttpGet]
        [Route("allRequests")]
        public IActionResult GetAllRequests()
        {
            // Check if a valid token is present
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized("Invalid token");
            }

            var result = _requestServices.GetAllRequests();
            return Ok(result);
        }

        [HttpPost]
        [Route("createRequest")]
        public IActionResult CreateRequest([FromBody] Request request)
        {
            try
            {
                // Check if a valid token is present
                if (!User.Identity.IsAuthenticated)
                {
                    return Unauthorized("Invalid token");
                }

                if (request == null)
                {
                    return BadRequest("Invalid request data");
                }

                _requestServices.CreateRequest(request);

                return Ok("Request created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
