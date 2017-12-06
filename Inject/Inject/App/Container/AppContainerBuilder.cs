using Inject.App;

namespace Inject
{
  public class AppContainerBuilder : IContainerBuilder
	{
    private readonly IDependencyContainer container;

    public AppContainerBuilder()
    {
      container = new AppDependencyContainer();
    }

		public IContainer Container => container;

		public void Define<TModule>() where TModule : IModule, new()
		{
			new TModule().Register(this);
		}

		public IRegisterEntity<TEntity> Register<TEntity>() where TEntity : class
		{
			return new AppRegisterEntity<TEntity>(container.Register<TEntity>());
		}
	}
}
