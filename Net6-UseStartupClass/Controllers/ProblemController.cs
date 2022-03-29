using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Net6_UseStartupClass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        private readonly ILogger<ProblemController> _logger;

        public ProblemController(ILogger<ProblemController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("[action]")]
        public IActionResult ProblemDetails()
        {
            var traceId = "TraceId";
            var activityId = "ActivityId";

            HttpContext.Items.Add(traceId, HttpContext.TraceIdentifier);
            HttpContext.Items.Add(activityId, Activity.Current?.Id);

            var correlationHeaders = Activity.Current?.Baggage.ToDictionary(b => b.Key, b => b.Value);

            _logger.LogInformation("Trace ID: {Item}", HttpContext.Items[traceId]);
            _logger.LogInformation("Activity ID: {Item}", HttpContext.Items[activityId]);

            var pd = new ProblemDetails()
            {
                Detail = "The request parameters failed to validate.",
                Instance = null,
                Status = 400,
                Title = "Validation Error",
                Type = "https://www.example.net/validation-error",
            };

            pd.Extensions.Add("TraceId", HttpContext.Items[traceId]);
            pd.Extensions.Add("ActivityId", HttpContext.Items[activityId]);
            pd.Extensions.Add("InvalidParams", new List<ValidationProblemDetailsParam>()
            {
                new("Name", "Cannot be blank."),
                new("Age", "Must be great or equals to 18.")
            });

            _logger.LogInformation("{@ProblemDetails}", pd);

            return new ObjectResult(pd) { StatusCode = pd.Status };
        }

        [HttpGet, Route("[action]")]
        public IActionResult Problem()
        {
            var traceId = "TraceId";
            var activityId = "ActivityId";

            HttpContext.Items.Add(traceId, HttpContext.TraceIdentifier);
            HttpContext.Items.Add(activityId, Activity.Current?.Id);

            var correlationHeaders = Activity.Current?.Baggage.ToDictionary(b => b.Key, b => b.Value);

            _logger.LogInformation("Trace ID: {Item}", HttpContext.Items[traceId]);
            _logger.LogInformation("Activity ID: {Item}", HttpContext.Items[activityId]);

            return Problem(detail: $"Trace ID: {HttpContext.Items[traceId]} -- Activity ID: {HttpContext.Items[activityId]}");
        }

        private class ValidationProblemDetailsParam
        {
            public ValidationProblemDetailsParam(string name, string reason)
            {
                Name = name;
                Reason = reason;
            }

            public string Name { get; set; }
            public string Reason { get; set; }
        }
    }
}
