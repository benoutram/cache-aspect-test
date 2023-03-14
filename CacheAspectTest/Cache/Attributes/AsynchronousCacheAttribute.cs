namespace CacheAspectTest.Cache.Attributes;

public class AsynchronousCacheAttribute : CacheAttribute
{
    public override object? GetItem(string key, Type returnType)
    {
        throw new NotImplementedException();
    }

    public override Task<object?> GetItemAsync(string key, Type returnType)
    {
        return CacheService.GetItemAsync(key, returnType);
    }
}
