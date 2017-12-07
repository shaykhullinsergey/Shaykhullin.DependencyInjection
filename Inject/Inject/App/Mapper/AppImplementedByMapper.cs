using System;

namespace Inject.App
{
  internal class AppImplementedByMapper<TEntity, TImplemented> : IImplementedByMapper<TEntity, TImplemented>
    where TEntity : class
    where TImplemented : class, TEntity
  {
    private readonly IDependencyInfo dependency;

    public AppImplementedByMapper(IDependencyInfo dependency)
    {
      this.dependency = dependency;
      dependency.Implemented = typeof(TImplemented);
    }

    public IReturnsMapper Returns(Func<IContainer, TImplemented> factory) =>
      new AppReturnsMapper(dependency, factory);

    public ILifeTimeMapper As<TLifeTime>() where TLifeTime : ILifeTime, new() =>
      new AppReturnsMapper(dependency)
        .As<TLifeTime>();

    public void For<TDependency>() where TDependency : class =>
      new AppReturnsMapper(dependency)
        .For<TDependency>();
  }
}