using System;
using System.Collections.Generic;

namespace DependencyInjection.Core
{
	internal class Container : IContainer
	{
		private readonly Container parent;
		private readonly Dictionary<Type, Dependency> dependencies;

		public Container(Dictionary<Type, Dependency> dependencies, Container parent)
		{
			this.dependencies = dependencies;
			this.parent = parent;
		}

		public TResolve Resolve<TResolve>()
			where TResolve : class
		{
			return (TResolve)Resolve(typeof(TResolve));
		}

		public object Resolve(Type type)
		{
			if (!dependencies.TryGetValue(type, out var dependency))
			{
				dependency = parent?.Find(type) ?? throw new InvalidOperationException($"{type} not found");
			}

			if (dependency.Instance == null)
			{
				dependency.Instance = (ILifecycle)Activator.CreateInstance(dependency.Lifecycle);
			}

			if (dependency.Factory != null)
			{
				return dependency.Instance.Resolve(() => dependency.Factory(this));
			}

			var ctor = dependency.Implemented.GetConstructors()[0];
			var parameters = ctor.GetParameters();
			var arguments = new object[parameters.Length];

			for (var i = 0; i < arguments.Length; i++)
			{
				arguments[i] = Resolve(parameters[i].ParameterType);
			}

			return dependency.Instance.Resolve(dependency.Implemented, arguments);
		}

		private Dependency Find(Type type)
		{
			return dependencies.TryGetValue(type, out var dependency)
				? dependency
				: parent?.Find(type);
		}
	}
}