using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Inject.App
{
  internal class AppDependencyInfo : IDependencyInfo
  {
    private IEnumerable<FieldInfo> injectFields;

    public AppDependencyInfo(Type entityType)
    {
      Entity = Implemented = entityType;
      LifeTime = new Transient();
    }

    public AppDependencyInfo(IDependencyInfo dependency)
    {
      Entity = dependency.Entity;
      Implemented = dependency.Implemented;
      Dependency = dependency.Dependency;
      Factory = dependency.Factory;
      LifeTime = WithoutParameters.Instance<ILifeTime>(dependency.LifeTime.GetType());
      injectFields = dependency.InjectFields;
    }

    public Type Entity { get; }
    public Type Implemented { get; set; }
    public Type Dependency { get; set; }
    public Func<IContainer, object> Factory { get; set; }
    public ILifeTime LifeTime { get; set; }
    public IEnumerable<FieldInfo> InjectFields => injectFields;
    public IEnumerable<FieldInfo> HierarchicalInjectFields =>
      injectFields ?? (injectFields = AllInjectFields(Implemented).ToList());

    private IEnumerable<FieldInfo> AllInjectFields(Type currentType)
    {
      if (currentType == typeof(object))
      {
        yield break;
      }

      var fields = currentType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
        .Where(f => f.IsDefined(typeof(InjectAttribute), false));

      foreach (var fieldInfo in fields)
      {
        yield return fieldInfo;
      }

      foreach (var fieldInfo in AllInjectFields(currentType.BaseType))
      {
        yield return fieldInfo;
      }
    }
  }
}