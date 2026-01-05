using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PublicLibrary.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/Test Controller for url versioning")]
    [ApiVersion("2.0")]
    public class TestController : ControllerBase
    {
        [HttpGet]

        public IActionResult Get()
        {
            return Ok("API is working!");
        }
    }
}
