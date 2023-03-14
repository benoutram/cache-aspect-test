using CacheAspectTest.Cache.Interfaces;

namespace CacheAspectTest.Cache;

public class TestCacheService : ICacheService
{
    private readonly ILogger<TestCacheService> _logger;

    public TestCacheService(ILogger<TestCacheService> logger)
    {
        _logger = logger;
    }

    public object? GetItem(string key, Type targetType)
    {
        return default;
    }

    public Task<object?> GetItemAsync(string key, Type targetType)
    {
        return Task.FromResult((object?) default);
    }

    public Task<object?> GetItemDelayedAsync(string key, Type targetType, int minDelay, int maxDelay)
    {
        var random = new Random();
        var delay = random.Next(minDelay, maxDelay);

        _logger.LogInformation("{service}.{method} starting (delay={delay})",
            nameof(TestCacheService),
            nameof(GetItemDelayedAsync),
            delay);

        return Task.Run(async () =>
        {
            await Task.Delay(delay);

            _logger.LogInformation("{service}.{method} ending (delay={delay})",
                nameof(TestCacheService),
                nameof(GetItemDelayedAsync),
                delay);

            return (object?) default;
        });
    }

    public void SetItem<TItem>(string key, TItem item)
    {
        // Intentionally not caching item
    }
}
