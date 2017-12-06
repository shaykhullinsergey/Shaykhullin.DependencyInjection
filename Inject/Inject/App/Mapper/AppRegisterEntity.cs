using System;

namespace Inject.App
{
	internal class AppRegisterEntity<TEntity> : IRegisterEntity<TEntity> 
		where TEntity : class
	{
		private readonly ILifeTimeDependency dependency;

		public AppRegisterEntity(ILifeTimeDependency dependency)
		{
			this.dependency = dependency;
		}

		public IImplementedBy<TEntity, TImplemented> ImplementedBy<TImplemented>() 
			where TImplemented : class, TEntity
		{
			return new AppImplementedBy<TEntity, TImplemented>(dependency);
		}

		public IReturnsMapper Returns(Func<IContainer, TEntity> factory)
		{
			return new AppImplementedBy<TEntity, TEntity>(dependency)
				.Returns(factory);
		}

		public ILifeTimeMapper As<TLifeTime>() where TLifeTime : ILifeTime, new()
		{
			return new AppImplementedBy<TEntity, TEntity>(dependency)
				.As<TLifeTime>();
		}

		public void For<TDependency>() where TDependency : class
		{
			new AppImplementedBy<TEntity, TEntity>(dependency)
				.For<TDependency>();
		}
	}
}
