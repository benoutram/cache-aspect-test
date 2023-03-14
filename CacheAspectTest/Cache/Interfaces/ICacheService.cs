namespace CacheAspectTest.Cache.Interfaces;

public interface ICacheService
{
    object? GetItem(string key, Type targetType);

    Task<object?> GetItemAsync(string key, Type targetType);

    Task<object?> GetItemDelayedAsync(string key, Type targetType, int minDelay, int maxDelay);

    void SetItem<TItem>(string key, TItem item);
}
