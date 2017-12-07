namespace Inject
{
	public interface IRegisterEntityMapper<TEntity> : IImplementedByMapper<TEntity, TEntity>
		where TEntity : class
	{
		IImplementedByMapper<TEntity, TImplemented> ImplementedBy<TImplemented>() 
			where TImplemented : class, TEntity;
	}
}
