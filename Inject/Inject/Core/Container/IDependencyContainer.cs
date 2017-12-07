using System.Collections.Generic;

namespace Inject
{
	interface IDependencyContainer : IContainer
	{
		List<IDependencyInfo> Dependencies { get; }
		IDependencyInfo Register<TEntity>();
	}
}
