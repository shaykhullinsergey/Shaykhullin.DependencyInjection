using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Inject.App
{
  internal class AppDependencyContainer : IDependencyContainer
  {
    private List<ILifeTimeDependency> dependencies = new List<ILifeTimeDependency>();

    public ILifeTimeDependency Register<TEntity>()
    {
      dependencies.Any(outer =>
        dependencies.Any(inner => 
          outer != inner && outer.Entity == inner.Entity && outer.Dependency == inner.Dependency)
            ? throw new TypeAlreadyRegisteredException(outer.Entity, outer.Implemented, outer.Dependency)
            : false);

      var dependency = new AppDependency<TEntity>();
      dependencies.Add(dependency);
      return dependency;
    }

    // Resolve<TEntity, TDependency> --> ResolveFor
    public TEntity Resolve<TEntity>() where TEntity : class
    {
      var dependency = dependencies
        .SingleOrDefault(d => d.Dependency == null && d.Entity == typeof(TEntity))
          ?? throw new TypeIsNotRegisteredException(typeof(TEntity), typeof(TEntity));

      var instance = dependency.LifeTime.Resolve<TEntity>(this, dependency);

      return ResolveRecursive<TEntity>(instance);
    }

    private TEntity ResolveRecursive<TEntity>(object instance)
    {
      var instanceType = instance.GetType();
      var privateFields = instanceType
        .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
        .Where(f => f.IsDefined(typeof(InjectAttribute), false))
        .Where(f => f.GetValue(instance) == null);

      foreach (var field in privateFields)
      {
        var fieldDependency = dependencies
          .SingleOrDefault(d => d.Dependency == instanceType && d.Entity == field.FieldType)
            ?? dependencies.SingleOrDefault(d => d.Dependency == null && d.Entity == field.FieldType)
            ?? throw new TypeIsNotRegisteredException(field.FieldType, instanceType);

        var fieldInstance = fieldDependency.LifeTime.Resolve<object>(this, fieldDependency);
        field.SetValue(instance, ResolveRecursive<object>(fieldInstance));
      }

      return (TEntity)instance;
    }
  }
}
