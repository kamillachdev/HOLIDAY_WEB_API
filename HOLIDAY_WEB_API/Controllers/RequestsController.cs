using HOLIDAY_WEB_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HOLIDAY_WEB_API.Controllers
{
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
        public IActionResult GetAllUsers()
        {
            var result = _requestServices.GetAllRequests();
            return Ok(result);
        }
    }
}
