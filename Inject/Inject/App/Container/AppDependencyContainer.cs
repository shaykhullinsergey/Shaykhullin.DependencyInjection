using System.Linq;
using System.Collections.Generic;

namespace Inject.App
{
  internal class AppDependencyContainer : IDependencyContainer
  {
    public AppDependencyContainer() => Dependencies = new List<IDependencyInfo>();

    public AppDependencyContainer(IDependencyContainer container) : this()
    {
      container.Dependencies.Any(outer =>
        container.Dependencies.Any(inner =>
          outer != inner && outer.Entity == inner.Entity && outer.Dependency == inner.Dependency)
            ? throw new TypeAlreadyRegisteredException(outer.Entity, outer.Implemented, outer.Dependency)
            : false);

      Dependencies.AddRange(container.Dependencies
        .Select(dependency => new AppDependencyInfo(dependency)));
    }
    public List<IDependencyInfo> Dependencies { get; }

    public IDependencyInfo Register<TEntity>()
    {
      var dependency = new AppDependencyInfo(typeof(TEntity));
      Dependencies.Add(dependency);
      return dependency;
    }

    public TEntity Resolve<TEntity>()
      where TEntity : class
    {
      var dependency = Dependencies
        .SingleOrDefault(d => d.Dependency == null && d.Entity == typeof(TEntity))
        ?? throw new TypeIsNotRegisteredException(typeof(TEntity));

      return ResolveRecursive<TEntity>(dependency);
    }

    public TEntity Resolve<TEntity, TDependency>()
      where TEntity : class
      where TDependency : class
    {
      var dependency = Dependencies
        .SingleOrDefault(d => d.Entity == typeof(TEntity) && d.Dependency == typeof(TDependency))
        ?? throw new TypeIsNotRegisteredException(typeof(TEntity), typeof(TDependency));

      return ResolveRecursive<TEntity>(dependency);
    }

    private TEntity ResolveRecursive<TEntity>(IDependencyInfo dependency)
    {
      var instance = dependency.LifeTime.Resolve<TEntity>(this, dependency);

      foreach (var field in dependency.HierarchicalInjectFields.Where(f => f.GetValue(instance) == null))
      {
        var fieldDependency = Dependencies
          .SingleOrDefault(d => d.Dependency == field.DeclaringType && d.Entity == field.FieldType)
            ?? Dependencies.SingleOrDefault(d => d.Dependency == null && d.Entity == field.FieldType)
            ?? throw new TypeIsNotRegisteredException(field.FieldType, field.DeclaringType);

        field.SetValue(instance, ResolveRecursive<object>(fieldDependency));
      }

      return instance;
    }
  }
}
