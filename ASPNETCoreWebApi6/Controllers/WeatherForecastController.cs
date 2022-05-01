using ASPNETCoreWebApi6.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ASPNETCoreWebApi6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private SmtpSettings _smtpSettings;
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            IOptionsSnapshot<SmtpSettings> smtpSettings)
        {
            _logger = logger;
            _smtpSettings = smtpSettings.Value;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            if (DateTime.Now.Second % 2 == 0)
            {
                throw new HttpResponseException()
                {
                    Status = 500,
                    Value = new
                    {
                        ErrorNo = 100,
                        ErrorMsg = "®É¶¡¿ù»~",
                        ErrorLink = "https://blog.miniasp.com"
                    }
                };
            }

            return data;
        }

        [HttpGet("~/config", Name = "GetConfig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public ActionResult<SmtpSettings> GetConfig()
        {
            if (this._smtpSettings == null)
            {
                return NotFound();
            }
            
            return Ok(this._smtpSettings);
        }
    }
}