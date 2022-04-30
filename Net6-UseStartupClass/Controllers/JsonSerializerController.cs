using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net6_UseStartupClass.Code.JsonSerializer;
using Newtonsoft.Json;

namespace Net6_UseStartupClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JsonSerializerController : ControllerBase
    {
        private readonly IApiJsonSerializer _apiJsonSerializer;

        public JsonSerializerController(IApiJsonSerializer apiJsonSerializer)
        {
            _apiJsonSerializer = apiJsonSerializer ?? throw new ArgumentNullException(nameof(apiJsonSerializer));
        }

        [HttpGet, Route("[action]")]
        public IActionResult Settings()
        {
            //by default, changes to the JSON configuration file after the app has started are read.
            var localJsonSerilizerSettings = _apiJsonSerializer.GetSettings<JsonSerializerSettings>();

            return Ok(localJsonSerilizerSettings);
        }
    }
}
