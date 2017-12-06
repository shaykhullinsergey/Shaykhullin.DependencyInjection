namespace Inject
{
	interface IDependencyContainer : IContainer
	{
		ILifeTimeDependency Register<TEntity>();
	}
}
