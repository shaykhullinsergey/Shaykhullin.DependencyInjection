using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Inject.App
{
  public static class WithoutParameters
  {
    private static readonly Dictionary<Type, Func<object>> cache =
      new Dictionary<Type, Func<object>>();

    public static object Instance(Type instanceType)
    {
      if (cache.TryGetValue(instanceType, out var factory))
      {
        return factory();
      }

      var ctor = instanceType.GetConstructor(Array.Empty<Type>());
      factory = Expression.Lambda<Func<object>>(Expression.New(ctor)).Compile();
      cache.Add(instanceType, factory);

      return factory();
    }

    public static TInstance Instance<TInstance>(Type instanceType)
    {
      return (TInstance)Instance(instanceType);
    }

    public static TInstance Instance<TInstance>()
    {
      return (TInstance)Instance(typeof(TInstance));
    }
  }
}
