using System.Reflection;
using AspectInjector.Broker;
using CacheAspectTest.Cache.Attributes;
using CacheAspectTest.Extensions;

namespace CacheAspectTest.Cache;

[Aspect(Scope.Global)]
public class CacheAspect
{
    private static readonly object NullMarker = new { __is_null = "$_is_null" };

    /// <summary>
    /// Enables cache attribute processing.
    /// <para>
    /// This is set to false by default so that test code
    /// isn't affected by this aspect. It should be set to
    /// true in your application startup, or if your tests
    /// are concerned with testing caching details.
    /// </para>
    /// </summary>
    public static bool Enabled { get; set; }

    [Advice(Kind.Around)]
    public object Handle(
        [Argument(Source.Target)] Func<object[], object> target,
        [Argument(Source.Arguments)] object[] args,
        [Argument(Source.ReturnType)] Type returnType,
        [Argument(Source.Triggers)] Attribute[] triggers)
    {
        if (!Enabled)
        {
            return target(args);
        }

        if (typeof(void) == returnType)
        {
            throw new ArgumentException("Method return type cannot be void");
        }

        var cacheTriggers = triggers
            .OfType<CacheAttribute>()
            .Distinct()
            .OrderBy(c => c.Priority)
            .ToList();
        var key = GetKey(target.Method, args);

        object? cachedResult = null;

        // May be using boxed types, and these may be nested, so we
        // recursively go through all the types to get the real
        // return type that we're interested in caching.
        var cachedReturnType = returnType.GetUnboxedResultTypePath().Last();

        foreach (var cacheTrigger in cacheTriggers)
        {
            // Determine which execution mode to use when getting an item from the cached service.
            // Synchronous or asynchronous.
            object? triggerResult;
            if (cacheTrigger is SynchronousCacheAttribute)
            {
                triggerResult = cacheTrigger.GetItem(key, cachedReturnType);
            }
            else
            {
                triggerResult = cacheTrigger.GetItemAsync(key, cachedReturnType).Result;
            }

            if (triggerResult is null)
            {
                continue;
            }

            cachedResult = triggerResult;
            break;
        }

        if (cachedResult?.TryBoxToResult(returnType, out cachedResult) == true)
        {
            return cachedResult;
        }

        // If no cached result could be found, then we run the real
        // method and get its result so that we can cache it.
        var result = target(args);

        // For the purpose of this test, don't unbox result to cachedResult because we can prove with
        // UnboxAspect that this code also has a problem.

        // if (!result.TryUnboxResult(out cachedResult) || cachedResult is null)
        // {
        //     return result;
        // }

        foreach (var cacheTrigger in cacheTriggers)
        {
            //cacheTrigger.SetItem(key, cachedResult);
            cacheTrigger.SetItem(key, result);
        }

        return result;
    }

    private static string GetKey(MethodInfo method, IEnumerable<object> args) =>
        $"{method.GetHashCode()}_{string.Join("_", args.Select(a => a?.GetHashCode() ?? NullMarker.GetHashCode()))}";
}
