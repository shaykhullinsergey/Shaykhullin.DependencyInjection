using System;

namespace DependencyInjection.Core
{
	internal class Container : IContainer
	{
		private readonly DependencyContainer dependencies;

		public Container(DependencyContainer dependencies)
		{
			this.dependencies = dependencies;
		}

		public TResolve Resolve<TResolve>()
			where TResolve : class
		{
			return (TResolve)Resolve(typeof(TResolve));
		}

		public object Resolve(Type type)
		{
			return ResolveRecursive(type, null);
		}

		private object ResolveRecursive(Type register, Type @for)
		{
			var dependency = dependencies.Get(register, @for) ?? throw new InvalidOperationException($"{register} not found");

			if (dependency.Instance == null)
			{
				dependency.Instance = (ILifecycle)Activator.CreateInstance(dependency.Lifecycle);
			}

			if (dependency.Factory != null)
			{
				return dependency.Instance.Resolve(() => dependency.Factory(this));
			}

			if (dependency.Parameters == null)
			{
				var ctor = dependency.Implemented.GetConstructors()[0];
				var parameters = ctor.GetParameters();
				
				dependency.Parameters = new Type[parameters.Length];
				for (var i = 0; i < parameters.Length; i++)
				{
					dependency.Parameters[i] = parameters[i].ParameterType;
				}
			}
			
			var arguments = new object[dependency.Parameters.Length];

			for (var i = 0; i < arguments.Length; i++)
			{
				arguments[i] = ResolveRecursive(dependency.Parameters[i], register);
			}

			return dependency.Instance.Resolve(dependency.Implemented, arguments);
		}
	}
}