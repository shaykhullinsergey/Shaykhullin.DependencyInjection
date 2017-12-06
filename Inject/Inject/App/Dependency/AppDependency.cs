using System;

namespace Inject.App
{
	internal class AppDependency<TEntity> : ILifeTimeDependency
	{
		public AppDependency()
		{
			Entity = Implemented = typeof(TEntity);
			LifeTime = new Transient();
		}

		public Type Entity { get; }
		public Type Implemented { get; set; }
		public Type Dependency { get; set; }
		public Func<IContainer, object> Factory { get; set; }
		public ILifeTime LifeTime { get; set; }
	}
}