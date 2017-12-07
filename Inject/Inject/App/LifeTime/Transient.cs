using System;

namespace Inject
{
  public class Transient : ILifeTime
  {
    public TEntity Resolve<TEntity>(IContainer container, IDependencyMetaInfo dependency)
    {
      if (dependency.Factory != null)
      {
        return (TEntity)dependency.Factory(container);
      }

      return (TEntity)Activator.CreateInstance(dependency.Implemented);

      //return dependency.Factory != null
      //	? (TEntity)dependency.Factory(container)
      //	: (TEntity)Activator.CreateInstance(dependency.Implemented);
    }
  }
}
