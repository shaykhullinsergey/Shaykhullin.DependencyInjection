namespace Inject
{
	public interface IContainer
	{
		TEntity Resolve<TEntity>()
      where TEntity : class;

		TEntity Resolve<TEntity, TDependency>()
			where TEntity : class
			where TDependency : class;
	}
}
