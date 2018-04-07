using System;
using System.Collections.Generic;

namespace DependencyInjection.Core
{
	internal class RegisterBuilder : IRegisterBuilder
	{
		private readonly Dictionary<Type, Dependency> dependencies;

		public RegisterBuilder(Dictionary<Type, Dependency> dependencies)
		{
			this.dependencies = dependencies;
		}
		
		public IImplementedByBuilder<TRegister> Register<TRegister>() 
			where TRegister : class
		{
			return Register(typeof(TRegister));
		}

		public IImplementedByBuilder<object> Register(Type register)
		{
			var dto = new Dependency(register);
			dependencies.Add(register, dto);
			return new ImplementedByBuilder<object>(dto);
		}
	}
}