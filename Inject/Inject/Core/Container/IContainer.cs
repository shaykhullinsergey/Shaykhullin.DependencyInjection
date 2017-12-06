namespace Inject
{
	public interface IContainer
	{
		TEntity Resolve<TEntity>() where TEntity : class;
	}
}
