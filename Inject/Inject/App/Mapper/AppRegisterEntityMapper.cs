using System;

namespace Inject.App
{
  internal class AppRegisterEntityMapper<TEntity> : IRegisterEntityMapper<TEntity>
    where TEntity : class
  {
    private readonly IDependencyInfo dependency;

    public AppRegisterEntityMapper(IDependencyInfo dependency) => this.dependency = dependency;

    public IImplementedByMapper<TEntity, TImplemented> ImplementedBy<TImplemented>()
      where TImplemented : class, TEntity =>
        new AppImplementedByMapper<TEntity, TImplemented>(dependency);

    public IReturnsMapper Returns(Func<IContainer, TEntity> factory) =>
      new AppImplementedByMapper<TEntity, TEntity>(dependency)
        .Returns(factory);

    public ILifeTimeMapper As<TLifeTime>()
      where TLifeTime : ILifeTime, new() =>
        new AppImplementedByMapper<TEntity, TEntity>(dependency)
          .As<TLifeTime>();

    public void For<TDependency>()
      where TDependency : class =>
        new AppImplementedByMapper<TEntity, TEntity>(dependency)
          .For<TDependency>();
  }
}
