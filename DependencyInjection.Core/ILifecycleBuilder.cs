using System;

namespace DependencyInjection.Core
{
	public interface ILifecycleBuilder
	{
		void As<TLifecycle>()
			where TLifecycle : ILifecycle;

		void As(Type lifecycle);
	}
}