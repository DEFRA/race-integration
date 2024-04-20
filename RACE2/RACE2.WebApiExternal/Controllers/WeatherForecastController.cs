using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace RACE2.WebApiExternal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly Serilog.ILogger _serilogLogger;

        public WeatherForecastController( Serilog.ILogger serilogLogger)
        {
            _serilogLogger = serilogLogger;
        }

        [Authorize]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // The following line demonstrates how we could use serilog's
            // own abstraction. Offers more features than ASP.NET core logging.
            _serilogLogger
                .ForContext("Controller", nameof(WeatherForecastController))
                .ForContext("Method", nameof(Get))
                .Warning("Entered");

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}