using System;
using Inject.App;

namespace Inject
{
  public class AppContainerBuilder : IContainerBuilder
  {
    private IDependencyContainer container;
    private bool firstTime = true;

    public AppContainerBuilder()
    {
      container = new AppDependencyContainer();
    }

    public IContainer Container
    {
      get
      {
        if (firstTime)
        {
          firstTime = false;
          return container;
        }

        return container = new AppDependencyContainer(container);
      }
    }

    public void Callback(Action<IContainerBuilder> callback) => callback(this);

    public void Module<TModule>()
      where TModule : IModule, new()
    {
      new TModule().Register(this);
    }

    public IRegisterEntityMapper<TEntity> Register<TEntity>()
      where TEntity : class
    {
      return new AppRegisterEntityMapper<TEntity>(container.Register<TEntity>());
    }
  }
}
