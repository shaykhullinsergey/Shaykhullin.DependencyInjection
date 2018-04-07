using System;
using System.Collections.Generic;
using DependencyInjection.Core;

namespace DependencyInjection
{
	public class ContainerConfig : IContainerConfig
	{
		private IContainer container;
		private readonly IContainerConfig parent;
		private readonly Dictionary<Type, Dependency> dependencies;

		public ContainerConfig()
		{
			dependencies = new Dictionary<Type, Dependency>();
		}

		internal ContainerConfig(IContainerConfig parent) 
			: this()
		{
			this.parent = parent;
		}

		public IImplementedByBuilder<TRegister> Register<TRegister>()
			where TRegister : class =>
				new RegisterBuilder(dependencies)
					.Register<TRegister>();

		public IImplementedByBuilder<object> Register(Type register) =>
			new RegisterBuilder(dependencies)
				.Register(register);

		public IContainerConfig Scope() => new ContainerConfig(this);

		public IContainer Container => container ?? (container = new Container(dependencies, (Container)parent?.Container));

		public void Dispose()
		{
		}
	}
}