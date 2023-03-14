using CacheAspectTest.Cache.Attributes;
using CacheAspectTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CacheAspectTest.Controllers;

[ApiController]
[Route("api/cache/sync/weather")]
public class WeatherForecastCacheSyncController : ControllerBase
{
    private readonly IWeatherForecastService _service;

    public WeatherForecastCacheSyncController(IWeatherForecastService service)
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

    [SynchronousCache]
    private IEnumerable<WeatherForecast> GetActualWeatherForecast()
    {
        return _service.GetForecast();
    }

    [SynchronousCache]
    private Task<IEnumerable<WeatherForecast>> GetActualWeatherForecastAsync()
    {
        return _service.GetForecastAsync();
    }

    [SynchronousCache]
    private Task<IEnumerable<WeatherForecast>> GetActualWeatherForecastDelayedAsync()
    {
        return _service.GetForecastDelayedAsync();
    }
}
