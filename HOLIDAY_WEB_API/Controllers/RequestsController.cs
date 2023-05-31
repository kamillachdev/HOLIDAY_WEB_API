using HOLIDAY_WEB_API.Interfaces;
using HOLIDAY_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace HOLIDAY_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestServices _requestServices;

        public RequestsController(IRequestServices requestServices, IRequestServices cre)
        {
            _requestServices = requestServices;
        }

        [HttpGet]
        [Route("allRequests")]
        [Authorize]
        public IActionResult GetAllUsers()
        {
            var result = _requestServices.GetAllRequests();
            return Ok(result);
        }

        [HttpPost]
        [Route("createRequest")]
        [Authorize]
        public IActionResult CreateRequest([FromBody] Request request)
        {
            try
            {
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
