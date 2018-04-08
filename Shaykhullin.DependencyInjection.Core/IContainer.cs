using System;

namespace DependencyInjection
{
	public interface IContainer
	{
		object Resolve(Type type);

		TResolve Resolve<TResolve>()
			where TResolve : class;
	}
}