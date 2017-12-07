using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inject.Tests
{
  [TestClass]
  public class ResolveRecursiveTests
  {
    private IContainerBuilder builder;

    [TestInitialize]
    public void TestInitialize()
    {
      builder = new AppContainerBuilder();
    }

    [TestMethod]
    public void ReturnsWithInjectWorks()
    {
      var a = new A();

      builder.Register<A>()
        .Returns(container => a);
      builder.Register<B>();
      builder.Register<C>();

      var c = builder.Container.Resolve<C>();

      Assert.AreEqual(a, c.B.A);
    }

    private class A { }
    private class B
    {
      [Inject]
      private A a;

      public A A => a;
    }
    private class C
    {
      [Inject]
      private B b;

      public B B => b;
    }
  }
}
