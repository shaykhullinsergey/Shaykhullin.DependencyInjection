using System;

namespace Inject.App
{
  internal class AppReturnsMapper : IReturnsMapper
  {
    private readonly IDependencyInfo dependency;

    public AppReturnsMapper(IDependencyInfo dependency) => this.dependency = dependency;

    public AppReturnsMapper(IDependencyInfo dependency, Func<IContainer, object> factory)
      : this(dependency) => dependency.Factory = factory;

    public ILifeTimeMapper As<TLifeTime>()
      where TLifeTime : ILifeTime, new() =>
        new AppLifeTimeMapper(dependency, new TLifeTime());

    public void For<TDependency>()
      where TDependency : class =>
        new AppLifeTimeMapper(dependency)
          .For<TDependency>();
  }
}