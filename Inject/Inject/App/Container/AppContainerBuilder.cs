using System;
using Inject.App;

namespace Inject
{
  public class AppContainerBuilder : IContainerBuilder
  {
    private readonly IDependencyContainer container;

    public AppContainerBuilder() => container = new AppDependencyContainer();

    public IContainer Container => new AppDependencyContainer(container);

    public void Callback(Action<IContainerBuilder> callback) => callback(this);

    public void Module<TModule>()
      where TModule : IModule, new() =>
        new TModule().Register(this);

    public IRegisterEntityMapper<TEntity> Register<TEntity>()
      where TEntity : class =>
        new AppRegisterEntityMapper<TEntity>(container.Register<TEntity>());
  }
}
