using Inject.App;
using System;

namespace Inject
{
  public class Singleton : ILifeTime
  {
    private object instance;

    public bool ResolveAgain { get; private set; }

    public TEntity Resolve<TEntity>(IContainer container, IDependencyMetaInfo dependency)
    {
      if (instance == null)
      {
        if (dependency.Factory != null)
        {
          instance = dependency.Factory(container);
        }

        instance = WithoutParameters.Instance(dependency.Implemented);
      }
      else
      {
        ResolveAgain = false;
      }


      return (TEntity)instance;

      //(TEntity)(instance ??
      //	(dependency.Factory != null
      //		? (instance = dependency.Factory(container))
      //		: (instance = Activator.CreateInstance(dependency.Implemented))));
    }
  }
}
