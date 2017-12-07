using System;

namespace Inject
{
  public class Singleton : ILifeTime
  {
    private object instance;

    public TEntity Resolve<TEntity>(IContainer container, IDependencyMetaInfo dependency)
    {
      if (instance == null)
      {
        if (dependency.Factory != null)
        {
          instance = dependency.Factory(container);
        }

        instance = Activator.CreateInstance(dependency.Implemented);
      }

      return (TEntity)instance;

      //(TEntity)(instance ??
      //	(dependency.Factory != null
      //		? (instance = dependency.Factory(container))
      //		: (instance = Activator.CreateInstance(dependency.Implemented))));
    }
  }
}
