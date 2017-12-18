namespace Inject
{
  public interface ILifeTimeMapper
	{
		void For<TDependency>()
      where TDependency : class;
	}
}
