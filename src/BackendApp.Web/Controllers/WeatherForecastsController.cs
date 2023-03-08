using Microsoft.AspNetCore.Mvc;

namespace BackendApp.Web.Controllers;

[ApiController]
[Route("weather-forecasts")]
public class WeatherForecastsController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet]
    public IActionResult Get()
    {
        var weatherForecasts = Enumerable.Range(1, 5)
            .Select(static index => ToWeatherForecast(index))
            .ToArray();

        return Ok(weatherForecasts);
    }

    private static WeatherForecast ToWeatherForecast(int index)
    {
        var date = DateTime.Now.AddDays(index);
        var temperatureC = Random.Shared.Next(-20, 55);
        var summary = Summaries[Random.Shared.Next(Summaries.Length)];

        return new WeatherForecast(date, temperatureC, summary);
    }
}
