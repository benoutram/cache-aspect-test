using AspectInjector.Broker;

namespace CacheAspectTest.Cache.Attributes;

[AttributeUsage(AttributeTargets.Method)]
[Injection(typeof(NoOpAspect))]
public class NoOpAttribute : Attribute
{
}
