using HOLIDAY_WEB_API.Models;
using HOLIDAY_WEB_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace HOLIDAY_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestServices _requestServices;

        public RequestsController(IRequestServices requestServices, IRequestServices cre)
        {
            _requestServices = requestServices;
        }

        [HttpGet]
        [Route("allRequests")]
        public IActionResult GetAllUsers()
        {
            var result = _requestServices.GetAllRequests();
            return Ok(result);
        }

        [HttpPost]
        [Route("createRequest")]
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
