using System;
using Inject.App;

namespace Inject
{
  public class AppContainerBuilder : IContainerBuilder
  {
    private IDependencyContainer container;
    private bool needContainer = false;

    public AppContainerBuilder()
    {
      container = new AppDependencyContainer();
    }

    public IContainer Container
    {
      get
      {
        foreach (var outer in container.Dependencies)
          foreach (var inner in container.Dependencies)
            if (outer != inner)
              if (outer.Entity == inner.Entity && outer.Dependency == inner.Dependency)
                throw new TypeAlreadyRegisteredException(outer.Entity, outer.Implemented, outer.Dependency);

        needContainer = true;
        return container;
      }
    }

    public void Callback(Action<IContainerBuilder> callback) 
    {
      callback(this);
    }

    public void Module<TModule>()
      where TModule : IModule, new()
    {
      new TModule().Register(this);
    }

    public IRegisterEntityMapper<TEntity> Register<TEntity>()
      where TEntity : class
    {
      if (needContainer)
      {
        container = new AppDependencyContainer(container);
      }

      var dependency = container.Register<TEntity>();
      return new AppRegisterEntityMapper<TEntity>(dependency);
    }
  }
}
