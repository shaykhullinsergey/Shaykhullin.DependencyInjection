using System.Linq;
using System.Collections.Generic;

namespace Inject.App
{
  internal class AppDependencyContainer : IDependencyContainer
  {
    public AppDependencyContainer() => Dependencies = new List<IDependencyInfo>();

    public AppDependencyContainer(IDependencyContainer container) : this()
    {
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
      for (int index = 0; index < Dependencies.Count; index++)
      {
        var dependency = Dependencies[index];

        if (dependency.Dependency == null && dependency.Entity == typeof(TEntity))
        {
          return ResolveRecursive<TEntity>(dependency);
        }
      }

      throw new TypeIsNotRegisteredException(typeof(TEntity));
    }

    public TEntity Resolve<TEntity, TDependency>()
      where TEntity : class
      where TDependency : class
    {
      for (int index = 0; index < Dependencies.Count; index++)
      {
        var dependency = Dependencies[index];

        if (dependency.Entity == typeof(TEntity) && dependency.Dependency == typeof(TDependency))
        {
          return ResolveRecursive<TEntity>(dependency);
        }
      }

      throw new TypeIsNotRegisteredException(typeof(TEntity), typeof(TDependency));
    }

    private TEntity ResolveRecursive<TEntity>(IDependencyInfo dependency)
    {
      var instance = dependency.LifeTime.Resolve<TEntity>(this, dependency);

      if(dependency.LifeTime.ResolveAgain)
      {
        foreach (var field in dependency.HierarchicalInjectFields)
        {
          var fieldDependency = Dependencies
            .FirstOrDefault(d => d.Dependency == field.DeclaringType && d.Entity == field.FieldType)
              ?? Dependencies.FirstOrDefault(d => d.Dependency == null && d.Entity == field.FieldType)
              ?? throw new TypeIsNotRegisteredException(field.FieldType, field.DeclaringType);

          field.SetValue(instance, ResolveRecursive<object>(fieldDependency));
        }
      }

      return instance;
    }
  }
}
