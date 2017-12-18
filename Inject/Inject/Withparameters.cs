using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Inject.App
{
  public static class WithParameters
  {
    private static readonly Dictionary<Type, Func<object[], object>> cache =
      new Dictionary<Type, Func<object[], object>>();

    public static object Instance(Type instanceType, object[] args)
    {
      if (cache.TryGetValue(instanceType, out var factory))
      {
        return factory(args);
      }

      factory = CreateFactory(instanceType);
      cache.Add(instanceType, factory);

      return factory(args);
    }

    public static TInstance Instance<TInstance>(Type instanceType, object[] args)
    {
      return (TInstance)Instance(instanceType, args);
    }

    public static TInstance Instance<TInstance>(object[] args)
    {
      return (TInstance)Instance(typeof(TInstance), args);
    }

    private static Func<object[], object> CreateFactory(Type type)
    {
      var ctor = type.GetConstructors().Single();
      var paramsInfo = ctor.GetParameters();
      var parameter = Expression.Parameter(typeof(object[]), "args");
      var argsExp = new Expression[paramsInfo.Length];

      for (int i = 0; i < paramsInfo.Length; i++)
      {
        var index = Expression.Constant(i);
        var paramType = paramsInfo[i].ParameterType;

        argsExp[i] = Expression.Convert(
          Expression.ArrayIndex(parameter, index),
          paramType);
      }

      return Expression.Lambda<Func<object[], object>>(
        Expression.New(ctor, argsExp), parameter)
        .Compile();
    }
  }
}
