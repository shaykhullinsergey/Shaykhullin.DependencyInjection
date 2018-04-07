using System;

namespace DependencyInjection.Core
{
	public interface IRegisterBuilder
	{
		IImplementedByBuilder<TRegister> Register<TRegister>()
			where TRegister : class;

		IImplementedByBuilder<object> Register(Type register);
	}
}