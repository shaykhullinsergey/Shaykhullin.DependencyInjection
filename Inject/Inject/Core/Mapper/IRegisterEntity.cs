namespace Inject
{
	public interface IRegisterEntity<TEntity> : IImplementedBy<TEntity, TEntity>
		where TEntity : class
	{
		IImplementedBy<TEntity, TImplemented> ImplementedBy<TImplemented>() 
			where TImplemented : class, TEntity;
	}
}
