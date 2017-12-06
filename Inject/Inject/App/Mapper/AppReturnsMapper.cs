using System;

namespace Inject.App
{
	internal class AppReturnsMapper : IReturnsMapper
	{
		private readonly ILifeTimeDependency dependency;

		public AppReturnsMapper(ILifeTimeDependency dependency, Func<IContainer, object> factory)
		{
			dependency.Factory = factory;
			this.dependency = dependency;
		}

		public ILifeTimeMapper As<TLifeTime>() where TLifeTime : ILifeTime, new()
		{
			return new AppLifeTimeMapper(dependency, new TLifeTime());
		}

		public void For<TDependency>() where TDependency : class
		{
			new AppLifeTimeMapper(dependency, new Transient())
				.For<TDependency>();
		}
	}
}