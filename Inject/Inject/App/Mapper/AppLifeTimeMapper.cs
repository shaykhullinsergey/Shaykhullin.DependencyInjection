namespace Inject.App
{
  internal class AppLifeTimeMapper : ILifeTimeMapper
  {
    private readonly IDependencyInfo dependency;

    public AppLifeTimeMapper(IDependencyInfo dependency) => this.dependency = dependency;

    public AppLifeTimeMapper(IDependencyInfo dependency, ILifeTime lifeTime)
      : this(dependency) => dependency.LifeTime = lifeTime;

    public void For<TDependency>()
      where TDependency : class =>
        dependency.Dependency = typeof(TDependency);
  }
}