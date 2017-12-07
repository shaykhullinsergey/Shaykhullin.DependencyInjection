using System;

namespace Inject
{
	public class TypeIsNotRegisteredException : Exception
	{
		public TypeIsNotRegisteredException(Type type, Type dependency = null)
		  : base($"{type.Name} for {dependency?.Name ?? "any type"} is not registered in container")
		{

		}
	}

	public class TypeAlreadyRegisteredException : Exception
	{
		public TypeAlreadyRegisteredException(Type entity, Type implemented, Type dependency = null)
		  : base($"{entity.Name} : {implemented?.Name} for {dependency?.Name ?? "any type"} already registered in container")
		{
		}
	}
}
