using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Net6_UseStartupClass.Code;

namespace Net6_UseStartupClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AppSettings? _appSettingsOptions { get; private set; }
        public TokenSettings? _tokenOptions { get; private set; }      

        public ConfigsController(IConfiguration configuration, IOptions<AppSettings> appSettingsOptions, IOptions<TokenSettings> tokenOptions)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _appSettingsOptions = appSettingsOptions.Value;
            _tokenOptions = tokenOptions.Value;
        }

        [HttpGet, Route("[action]")]
        public IActionResult Bind()
        {
            //by default, changes to the JSON configuration file after the app has started are read.
            var localAppSettings = new AppSettings();
            _configuration.GetSection(AppSettings.Key).Bind(localAppSettings);

            return Ok(localAppSettings);
        }

        [HttpGet, Route("[action]")]
        public IActionResult OnGet()
        {
            //by default, changes to the JSON configuration file after the app has started are read.
            var localAppSettings = _configuration.GetSection(AppSettings.Key).Get<AppSettings>();

            return Ok(localAppSettings);
        }

        [HttpGet, Route("[action]")]
        public IActionResult ConfigureOption()
        {
            return Ok(_appSettingsOptions);
        }

        [HttpGet, Route("[action]")]
        public IActionResult AddOption()
        {
            return Ok(_tokenOptions);
        }


    }
}
