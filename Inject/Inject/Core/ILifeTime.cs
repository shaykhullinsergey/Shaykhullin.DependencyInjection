namespace Inject
{
	public interface ILifeTime
	{
    bool ResolveAgain { get; }
		TEntity Resolve<TEntity>(IContainer container, IDependencyMetaInfo dependency);
	}
}
