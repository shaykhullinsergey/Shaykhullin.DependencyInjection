namespace Inject.App
{
	internal class AppLifeTimeMapper : ILifeTimeMapper
	{
		private readonly ILifeTimeDependency dependency;

		public AppLifeTimeMapper(ILifeTimeDependency dependency, ILifeTime lifeTime)
		{
			dependency.LifeTime = lifeTime;
			this.dependency = dependency;
		}

		public void For<TDependency>()
      where TDependency : class
		{
			dependency.Dependency = typeof(TDependency);
		}
	}
}