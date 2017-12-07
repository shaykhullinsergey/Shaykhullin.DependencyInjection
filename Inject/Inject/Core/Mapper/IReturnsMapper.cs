namespace Inject
{
	public interface IReturnsMapper : ILifeTimeMapper
	{
		ILifeTimeMapper As<TLifeTime>() 
			where TLifeTime : ILifeTime, new();
	}
}
