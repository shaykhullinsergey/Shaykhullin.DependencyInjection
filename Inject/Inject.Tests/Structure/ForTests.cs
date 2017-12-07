using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inject.Tests
{
  [TestClass]
  public class ForTests
  {
    private IContainerBuilder builder;

    [TestInitialize]
    public void TestInitialize()
    {
      builder = new AppContainerBuilder();
    }

    [TestMethod]
    public void ForWorksCorrect()
    {
      var aForA = new A();
      var aForB = new A();
      var aForC = new A();

      builder.Register<A>()
        .Returns(container => aForA);

      builder.Register<A>()
        .Returns(container => aForB)
        .For<B>();

      builder.Register<A>()
        .Returns(container => aForC)
        .For<C>();

      builder.Register<B>();
      builder.Register<C>();

      var a = builder.Container.Resolve<A>();
      var b = builder.Container.Resolve<B>();
      var c = builder.Container.Resolve<C>();

      Assert.AreEqual(aForA, a);
      Assert.AreEqual(aForB, b.A);
      Assert.AreEqual(aForC, c.A);
    }

    private class A
    {
    }
    private class B
    {
      [Inject]
      private A a;
      public A A => a;
    }
    private class C
    {
      [Inject]
      private A a;
      public A A => a;
    }
  }
}
