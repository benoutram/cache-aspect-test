using AspectInjector.Broker;

namespace CacheAspectTest.Cache.Attributes;

[AttributeUsage(AttributeTargets.Method)]
[Injection(typeof(UnboxAspect))]
public class UnboxAttribute : Attribute
{
}
