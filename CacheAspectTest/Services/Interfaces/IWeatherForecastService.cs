namespace CacheAspectTest.Services.Interfaces;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> GetForecast();

    Task<IEnumerable<WeatherForecast>> GetForecastAsync();

    Task<IEnumerable<WeatherForecast>> GetForecastDelayedAsync(int minDelay = 1, int maxDelay = 101);
}
