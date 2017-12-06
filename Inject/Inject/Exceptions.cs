using System;

namespace Inject
{
  public class TypeIsNotRegisteredException : Exception
  {
    public TypeIsNotRegisteredException(Type type, Type dependency) 
      : base($"{type.Name} for {dependency.Name} is not registered in container")
    {
    }
  }

  public class TypeAlreadyRegisteredException : Exception
  {
    public TypeAlreadyRegisteredException(Type entity, Type implemented, Type dependency)
      : base($"{entity.Name} : {implemented.Name} for {dependency.Name}already registered in container")
    {
    }
  }
}
