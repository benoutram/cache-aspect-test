using AspectInjector.Broker;

namespace CacheAspectTest.Cache;

[Aspect(Scope.Global)]
public class NoOpAspect
{
    [Advice(Kind.Around)]
    public object Handle(
        [Argument(Source.Target)] Func<object[], object> target,
        [Argument(Source.Arguments)] object[] args,
        [Argument(Source.ReturnType)] Type returnType,
        [Argument(Source.Triggers)] Attribute[] triggers)
    {
        return target(args);
    }
}
