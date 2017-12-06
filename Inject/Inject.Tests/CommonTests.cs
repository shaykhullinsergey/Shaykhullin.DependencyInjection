using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inject.Tests
{
  [TestClass]
  public class CommonTests
  {
    private IContainerBuilder builder;

    [TestInitialize]
    public void TestInitialize()
    {
      builder = new AppContainerBuilder();
    }

    [TestMethod]
    public void AIsRegistering()
    {
      builder.Register<A>();

      var c = builder.Container.Resolve<A>();

      Assert.IsNotNull(c);
    }

    [TestMethod]
    public void DublicateThrows()
    {
      builder.Register<A>();
      builder.Register<A>()
        .ImplementedBy<B>();

      var container = builder.Container;
    }

    [TestMethod]
    public void AIsImplementedByB()
    {
      builder.Register<A>()
        .ImplementedBy<B>();

      var a = builder.Container.Resolve<A>();

      Assert.IsTrue(a is B);
    }

    [TestMethod]
    public void ReturnsWorks()
    {
      var a = new A();

      builder.Register<A>()
        .Returns(c => a);

      var resolvedA = builder.Container.Resolve<A>();

      Assert.AreEqual(a, resolvedA);
    }

    [TestMethod]
    public void ReturnsWithImplementedWorks()
    {
      var b = new B();

      builder.Register<A>()
        .ImplementedBy<B>()
        .Returns(c => b);

      var resolvedA = builder.Container.Resolve<A>();

      Assert.AreEqual(b, resolvedA);
    }

    [TestMethod]
    public void SingletonWorks()
    {
      builder.Register<A>()
        .As<Singleton>();

      var a1 = builder.Container.Resolve<A>();
      var a2 = builder.Container.Resolve<A>();

      Assert.AreEqual(a1, a2);
    }

    [TestMethod]
    public void SingletonWithImplementedWorks()
    {
      builder.Register<A>()
        .ImplementedBy<B>()
        .As<Singleton>();

      var a1 = builder.Container.Resolve<A>();
      var a2 = builder.Container.Resolve<A>();

      Assert.IsTrue(a1 is B);
    }

    [TestMethod]
    public void TransientWorks()
    {
      builder.Register<A>()
        .As<Transient>();

      var a1 = builder.Container.Resolve<A>();
      var a2 = builder.Container.Resolve<A>();

      Assert.AreNotEqual(a1, a2);
    }

    [TestMethod]
    public void TransientWithImplementedWorks()
    {
      builder.Register<A>()
        .ImplementedBy<B>()
        .As<Transient>();

      var a1 = builder.Container.Resolve<A>();
      var a2 = builder.Container.Resolve<A>();

      Assert.IsTrue(a1 is B);
    }

    private class A { }
    private class B : A { }
    private class C { }
  }
}
