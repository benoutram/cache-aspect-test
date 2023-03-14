namespace CacheAspectTest.Cache.Attributes;

public class SynchronousCacheAttribute : CacheAttribute
{
    public override object? GetItem(string key, Type returnType)
    {
        return CacheService.GetItem(key, returnType);
    }

    public override Task<object?> GetItemAsync(string key, Type returnType)
    {
        throw new NotImplementedException();
    }
}
