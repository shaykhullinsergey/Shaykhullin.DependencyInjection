using System;
using DependencyInjection.Core;

namespace DependencyInjection
{
	public class Singleton : ILifecycle
	{
		private object instance;
		
		public object Resolve(Type type, object[] arguments)
		{
			return instance ?? (instance = Activator.CreateInstance(type, arguments));
		}

		public object Resolve(Func<object> factory)
		{
			return instance ?? (instance = factory());
		}
	}
}