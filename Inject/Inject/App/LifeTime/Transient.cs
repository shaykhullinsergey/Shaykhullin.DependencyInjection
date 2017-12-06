using System;

namespace Inject
{
  public class Transient : ILifeTime
	{
    public TEntity Resolve<TEntity>(IContainer container, IDependency dependency)
    {
      if(dependency.Factory != null)
      {
        return (TEntity)dependency.Factory(container);
      }

      return (TEntity) Activator.CreateInstance(dependency.Implemented);
    }
  }
}
