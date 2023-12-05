using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/demo")]
    public class DemoApiController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<DemoApiController> _logger;

        public DemoApiController(ILogger<DemoApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("LogJSONObject")]
        public WeatherForecast LogJSONObject()
        {
            var result = new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
            //Query can be used with full name of properties
            //eg. Object.Date, Object.TemperatureC, Object.Summary
            _logger.LogInformation("Request Object: {@Object}", result);


            return result;
        }

        [HttpGet]
        [Route("LogArrayObject")]
        public IEnumerable<WeatherForecast> LogArrayObject()
        {
            var result = new Array[5].Select((obj, index) => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
            _logger.LogInformation("Request Object Array: {@Array}", result);
            return result;
        }
    }
}
