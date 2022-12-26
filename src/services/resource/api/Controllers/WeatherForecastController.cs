using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [Authorize(Policy = "WeatherForecastForInstantPolicy")]
        [Route("/instant")]
        [HttpGet]
        public IActionResult GetForNow()
        {
            return Ok(new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55)
            });
        }

        [Authorize(Policy = "WeatherForecastFor5DaysPolicy")]
        [Route("/for5days")]
        [HttpGet]
        public IActionResult GetFor5Days()
        {
            return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55)
            })
            .ToArray());
        }

        [Authorize(Policy = "WeatherForecastFor10DaysPolicy")]
        [Route("/for10days")]
        [HttpGet]
        public IActionResult GetFor10Days()
        {
            return Ok(Enumerable.Range(1, 10).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55)
            })
            .ToArray());
        }
    }
}