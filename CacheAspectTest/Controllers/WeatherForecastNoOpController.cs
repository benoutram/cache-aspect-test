using CacheAspectTest.Cache.Attributes;
using CacheAspectTest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CacheAspectTest.Controllers;

[ApiController]
[Route("api/no-op/weather")]
public class WeatherForecastNoOpController : ControllerBase
{
    private readonly IWeatherForecastService _service;

    public WeatherForecastNoOpController(IWeatherForecastService service)
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

    [NoOp]
    private IEnumerable<WeatherForecast> GetActualWeatherForecast()
    {
        return _service.GetForecast();
    }

    [NoOp]
    private Task<IEnumerable<WeatherForecast>> GetActualWeatherForecastAsync()
    {
        return _service.GetForecastAsync();
    }

    [NoOp]
    private Task<IEnumerable<WeatherForecast>> GetActualWeatherForecastDelayedAsync()
    {
        return _service.GetForecastDelayedAsync();
    }
}
