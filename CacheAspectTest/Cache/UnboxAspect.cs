using AspectInjector.Broker;
using CacheAspectTest.Extensions;

namespace CacheAspectTest.Cache;

[Aspect(Scope.Global)]
public class UnboxAspect
{
    [Advice(Kind.Around)]
    public object Handle(
        [Argument(Source.Target)] Func<object[], object> target,
        [Argument(Source.Arguments)] object[] args,
        [Argument(Source.ReturnType)] Type returnType,
        [Argument(Source.Triggers)] Attribute[] triggers)
    {
        var result = target(args);
        if (result.TryUnboxResult(out var unboxedResult)) {
            Console.WriteLine($"Unboxed result of type {unboxedResult?.GetType().Name}");
        }
        else
        {
            Console.WriteLine("Failed to unbox result");
        }
        return result;
    }
}
