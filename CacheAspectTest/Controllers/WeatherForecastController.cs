using CacheAspectTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CacheAspectTest.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _service;

    public WeatherForecastController(IWeatherForecastService service)
    {
        _service = service;
    }

    [HttpGet("sync")]
    public IEnumerable<WeatherForecast> GetWeatherForecast()
    {
        return _service.GetForecast();
    }

    [HttpGet("async")]
    public Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
    {
        return _service.GetForecastAsync();
    }

    [HttpGet("async-delayed")]
    public Task<IEnumerable<WeatherForecast>> GetWeatherForecastDelayedAsync()
    {
        return _service.GetForecastDelayedAsync();
    }
}
