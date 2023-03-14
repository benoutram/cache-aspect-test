using CacheAspectTest.Cache.Attributes;
using CacheAspectTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CacheAspectTest.Controllers;

[ApiController]
[Route("api/unbox/weather")]
public class WeatherForecastUnboxController : ControllerBase
{
    private readonly IWeatherForecastService _service;

    public WeatherForecastUnboxController(IWeatherForecastService service)
    {
        _service = service;
    }

    [HttpGet("sync")]
    public IEnumerable<WeatherForecast> GetWeatherForecast()
    {
        return GetActualWeatherForecast();
    }

    [HttpGet("async")]
    public Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
    {
        return GetActualWeatherForecastAsync();
    }

    [HttpGet("async-delayed")]
    public Task<IEnumerable<WeatherForecast>> GetWeatherForecastDelayedAsync()
    {
        return GetActualWeatherForecastDelayedAsync();
    }

    [Unbox]
    private IEnumerable<WeatherForecast> GetActualWeatherForecast()
    {
        return _service.GetForecast();
    }

    [Unbox]
    private Task<IEnumerable<WeatherForecast>> GetActualWeatherForecastAsync()
    {
        return _service.GetForecastAsync();
    }

    [Unbox]
    private Task<IEnumerable<WeatherForecast>> GetActualWeatherForecastDelayedAsync()
    {
        return _service.GetForecastDelayedAsync();
    }
}
