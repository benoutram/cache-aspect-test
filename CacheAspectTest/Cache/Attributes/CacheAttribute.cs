using AspectInjector.Broker;
using CacheAspectTest.Cache.Interfaces;

namespace CacheAspectTest.Cache.Attributes;

/// <summary>
/// Base caching attribute that should be extended with specific
/// caching implementations e.g. blob storage, memory, Redis, etc.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
[Injection(typeof(CacheAspect), Inherited = true)]
public abstract class CacheAttribute : Attribute
{
    // Set during startup
    public static ICacheService CacheService { get; set; } = null!;

    /// <summary>
    /// Cache with highest priority is checked first. 0 - Highest, 255 - Lowest. Default 127
    /// </summary>
    public byte Priority { get; set; } = 127;

    public abstract object? GetItem(string key, Type returnType);

    public abstract Task<object?> GetItemAsync(string key, Type returnType);

    public void SetItem(string key, object value)
    {
        CacheService.SetItem(key, value);
    }
}
