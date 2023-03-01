using Microsoft.AspNetCore.Mvc;
using Weather.Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        }

        [HttpGet("weather/{city}")]
        public async Task<IActionResult> GetWeatherForCity(string city)
        {
            var weather = await _weatherService.GetCurrentWeatherAsync(city);

            if (weather is null)
            {
                return NotFound();
            }

            return Ok(weather);
        }
    }
}
