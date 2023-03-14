using CacheAspectTest.Services.Interfaces;

namespace CacheAspectTest.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly ILogger<WeatherForecastService> _logger;

    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public WeatherForecastService(ILogger<WeatherForecastService> logger)
    {
        _logger = logger;
    }

    public IEnumerable<WeatherForecast> GetForecast()
    {
        return GenerateRandomForecast();
    }

    public Task<IEnumerable<WeatherForecast>> GetForecastAsync()
    {
        return Task.FromResult(GenerateRandomForecast());
    }

    public Task<IEnumerable<WeatherForecast>> GetForecastDelayedAsync(int minDelay, int maxDelay)
    {
        var random = new Random();
        var delay = random.Next(minDelay, maxDelay);

        _logger.LogInformation("{service}.{method} starting (delay={delay})",
            nameof(WeatherForecastService),
            nameof(GetForecastDelayedAsync),
            delay);

        return Task.Run(async () =>
        {
            await Task.Delay(delay);
            var result = GenerateRandomForecast();

            _logger.LogInformation("{service}.{method} ending (delay={delay})",
                nameof(WeatherForecastService),
                nameof(GetForecastDelayedAsync),
                delay);

            return result;
        });
    }

    private static IEnumerable<WeatherForecast> GenerateRandomForecast()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}
