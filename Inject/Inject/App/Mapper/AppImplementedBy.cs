using System;

namespace Inject.App
{
	internal class AppImplementedBy<TEntity, TImplemented> : IImplementedBy<TEntity, TImplemented>
		where TEntity : class
		where TImplemented : class, TEntity
	{
		private readonly ILifeTimeDependency dependency;

		public AppImplementedBy(ILifeTimeDependency dependency)
		{
			dependency.Implemented = typeof(TImplemented);
			this.dependency = dependency;
		}

		public IReturnsMapper Returns(Func<IContainer, TImplemented> factory)
		{
			return new AppReturnsMapper(dependency, factory);
		}

		public ILifeTimeMapper As<TLifeTime>() where TLifeTime : ILifeTime, new()
		{
			return new AppReturnsMapper(dependency, null)
				.As<TLifeTime>();
		}

		public void For<TDependency>() where TDependency : class
		{
			new AppReturnsMapper(dependency, null)
				.For<TDependency>();
		}
	}
}