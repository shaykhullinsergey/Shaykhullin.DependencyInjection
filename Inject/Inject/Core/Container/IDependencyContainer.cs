using System.Collections.Generic;

namespace Inject
{
	interface IDependencyContainer : IContainer
	{
		IDependencyInfo Register<TEntity>();
		List<IDependencyInfo> Dependencies { get; }
	}
}
