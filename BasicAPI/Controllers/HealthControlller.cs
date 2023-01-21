using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    public class HealthControlller : ControllerBase
    {

        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            return Ok("Everything seems ok.");
        }
    }
}
