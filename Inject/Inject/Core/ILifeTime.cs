namespace Inject
{
	public interface ILifeTime
	{
		TEntity Resolve<TEntity>(IContainer container, IDependencyMetaInfo dependency);
	}
}
