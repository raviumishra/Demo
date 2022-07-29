using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Weather.Controllers
{
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] IrelandSummaries = new[]
        {
            "Spring", "Winter", "Summer", "Autumn"
        };

        private static readonly string[] IndiaSummaries = new[]
        {
            "Summer", "Winter", "Rainy"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("{region}/[controller]")]
        public IEnumerable<WeatherForecast> Get(string region)
        {
            return region switch
            {
                "IE" => GetIrelandSummaries(),
                "IN" => GetIndiaSummaries(),
                _ => throw new ArgumentException("Region not supported")
            };
        }

        [HttpGet]
        [Route("[controller]")]
        public IEnumerable<WeatherForecast> Get()
        {
            var region = _configuration.GetSection("REGION").Value;
            return region switch
            {
                "IE" => GetIrelandSummaries(),
                "IN" => GetIndiaSummaries(),
                _ => throw new ArgumentException("Region not supported")
            };
        }

        private static IEnumerable<WeatherForecast> GetIrelandSummaries()
        {
            var irelandRng = new Random();
            return Enumerable.Range(1, 1).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = irelandRng.Next(-20, 55),
                Summary = IrelandSummaries[irelandRng.Next(IrelandSummaries.Length)]
            }).ToArray();
        }

        private static IEnumerable<WeatherForecast> GetIndiaSummaries()
        {
            var indiaRng = new Random();
            return Enumerable.Range(1, 1).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = indiaRng.Next(-20, 55),
                Summary = IndiaSummaries[indiaRng.Next(IndiaSummaries.Length)]
            }).ToArray();
        }
    }
}