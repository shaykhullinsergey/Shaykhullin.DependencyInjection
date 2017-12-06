namespace Inject
{
	public interface IContainerBuilder
	{
		IContainer Container { get; }
		IRegisterEntity<TEntity> Register<TEntity>() where TEntity : class;
		void Define<TModule>() where TModule : IModule, new();
	}
}
