using CacheAspectTest.Cache.Attributes;
using CacheAspectTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CacheAspectTest.Controllers;

[ApiController]
[Route("api/cache/async/weather")]
public class WeatherForecastCacheAsyncController : ControllerBase
{
    private readonly IWeatherForecastService _service;

    public WeatherForecastCacheAsyncController(IWeatherForecastService service)
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

    [AsynchronousCache]
    private IEnumerable<WeatherForecast> GetActualWeatherForecast()
    {
        return _service.GetForecast();
    }

    [AsynchronousCache]
    private Task<IEnumerable<WeatherForecast>> GetActualWeatherForecastAsync()
    {
        return _service.GetForecastAsync();
    }

    [AsynchronousCache]
    private Task<IEnumerable<WeatherForecast>> GetActualWeatherForecastDelayedAsync()
    {
        return _service.GetForecastDelayedAsync();
    }
}
