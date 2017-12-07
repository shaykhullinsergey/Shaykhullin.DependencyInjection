using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
    public void ContainerEachTimeIsDifferent()
    {
      builder.Register<A>();
      var container1 = builder.Container;

      builder.Register<B>();
      var container2 = builder.Container;

      Assert.AreNotEqual(container1, container2);
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
      Assert.ThrowsException<TypeAlreadyRegisteredException>(() =>
      {
        builder.Register<A>();
        builder.Register<A>()
          .ImplementedBy<B>();

        var container = builder.Container;
      });
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

    public void CASDASD()
    {
      builder.Register<A>()
        .As<Singleton>();
      var c1 = builder.Container;

      builder.Register<A>()
        .As<Singleton>()
        .For<A>();
      var c2 = builder.Container;

      Console.WriteLine(ReferenceEquals(c1, c2));

      var a1 = c1.Resolve<A>();
      var a2 = c2.Resolve<A>();

      Console.WriteLine(ReferenceEquals(a1, a2));
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

      var container = builder.Container;


      var a1 = container.Resolve<A>();
      var a2 = container.Resolve<A>();

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
