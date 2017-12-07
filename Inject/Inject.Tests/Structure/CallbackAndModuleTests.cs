using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inject.Tests
{
  [TestClass]
  public class CallbackAndModuleTests
  {
    private class A { }

    [TestMethod]
    public void CallbackTest()
    {
      var builder = new AppContainerBuilder();

      builder.Callback(c => c.Register<A>());

      var container = builder.Container;

      var a = container.Resolve<A>();

      Assert.IsNotNull(a);
    }

    [TestMethod]
    public void ModuleTest()
    {
      var builder = new AppContainerBuilder();
      builder.Module<Module>();

      var container = builder.Container;

      var a = container.Resolve<A>();

      Assert.IsNotNull(a);
    }

    public class Module : IModule
    {
      public void Register(IContainerBuilder builder)
      {
        builder.Register<A>();
      }
    }
  }
}
