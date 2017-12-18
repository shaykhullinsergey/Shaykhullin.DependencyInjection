using Inject.App;
using System;

namespace Inject
{
  public class Transient : ILifeTime
  {
    public bool ResolveAgain => true;

    public TEntity Resolve<TEntity>(IContainer container, IDependencyMetaInfo dependency)
    {
      if (dependency.Factory != null)
      {
        return (TEntity)dependency.Factory(container);
      }

      return WithoutParameters.Instance<TEntity>(dependency.Implemented);

      //return dependency.Factory != null
      //	? (TEntity)dependency.Factory(container)
      //	: (TEntity)Activator.CreateInstance(dependency.Implemented);
    }
  }
}
