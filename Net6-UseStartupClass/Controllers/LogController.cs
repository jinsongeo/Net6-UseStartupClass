using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Net6_UseStartupClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("GetData")]
        public IActionResult Get()
        {
            // Log messages with structured content embedded into msg text.
            // The following line uses the ASP.NET core logging abstraction.
            _logger.LogInformation("Entered {Controller}.{Method}", nameof(LogController), nameof(Get));

            return Ok();
        }

        [HttpPost("SaveData")]
        public IActionResult Save(string data)
        {
            // Simulate adding to DB
            _logger.LogInformation("Saving Entity {EntityObject} to DB", data);

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
