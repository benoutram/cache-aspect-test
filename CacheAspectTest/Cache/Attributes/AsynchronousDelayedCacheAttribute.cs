namespace CacheAspectTest.Cache.Attributes;

public class AsynchronousDelayedCacheAttribute : CacheAttribute
{
    /// <summary>
    /// Minimum delay to introduce when getting an item from the cached service in milliseconds.
    /// </summary>
    public int MinDelay = 1;

    /// <summary>
    /// Maximum delay to introduce when getting an item from the cached service in milliseconds.
    /// </summary>
    public int MaxDelay = 101;

    public override object? GetItem(string key, Type returnType)
    {
        throw new NotImplementedException();
    }

    public override Task<object?> GetItemAsync(string key, Type returnType)
    {
        return CacheService.GetItemDelayedAsync(key, returnType, MinDelay, MaxDelay);
    }
}
