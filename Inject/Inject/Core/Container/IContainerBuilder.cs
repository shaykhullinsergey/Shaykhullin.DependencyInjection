using System;

namespace Inject
{
	public interface IContainerBuilder
	{
		IContainer Container { get; }

    IRegisterEntityMapper<TEntity> Register<TEntity>()
      where TEntity : class;

    void Module<TModule>()
      where TModule : IModule, new();

    void Callback(Action<IContainerBuilder> callback);
	}
}
