using DLS_Exam_Example.Model;
using Microsoft.AspNetCore.Mvc;

namespace DLS_Exam_Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Weather forecast requested at {Time}", DateTime.UtcNow);
            var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            _logger.LogDebug("Generated {Count} weather forecast records", forecast.Length);

            return forecast;
        }

        [HttpGet("test-log-trace")]
        public IActionResult TestTrace()
        {
            _logger.LogTrace("This is a TRACE-level message at {Time}", DateTime.UtcNow);
            return Ok("Trace level logged");
        }

        [HttpGet("test-log-warning")]
        public IActionResult TestWarning()
        {
            _logger.LogWarning("This is a WARNING-level message at {Time}. A possible issue might occur.", DateTime.UtcNow);
            return Ok("Warning level logged");
        }

        [HttpGet("test-log-error")]
        public IActionResult TestError()
        {
            try
            {
                int result = 10 / int.Parse("0"); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while dividing numbers at {Time}", DateTime.UtcNow);
                return StatusCode(500, "An error occurred. Check logs for details.");
            }

            return Ok("Error level logged");
        }

        [HttpGet("test-log-critical")]
        public IActionResult TestCritical()
        {
            _logger.LogCritical("This is a CRITICAL-level log! Immediate attention needed at {Time}", DateTime.UtcNow);
            return Ok("Critical level logged");
        }
    }
}
