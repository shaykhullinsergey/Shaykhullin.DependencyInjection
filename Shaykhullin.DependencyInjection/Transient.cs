using System;
using DependencyInjection.Core;

namespace DependencyInjection
{
	public class Transient : ILifecycle
	{
		public object Resolve(Type type, object[] arguments)
		{
			return Activator.CreateInstance(type, arguments);
		}

		public object Resolve(Func<object> factory)
		{
			return factory();
		}
	}
}