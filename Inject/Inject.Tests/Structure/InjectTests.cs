using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inject.Tests
{
  [TestClass]
  public class InjectTests
  {
    public class TestA
    {
      [Inject]
      private TestC c;

      [Inject]
      private TestD d;

      public TestA()
      {
        Console.WriteLine("Instantiate TestA");
      }

      public TestC C => c;
      public TestD D => d;
    }

    public class TestB : TestA
    {
      [Inject]
      private TestE e;

      [Inject]
      private TestD d;

      public TestB()
      {
        Console.WriteLine("Instantiate TestB");
      }
    }

    public class TestC
    {
      [Inject]
      private TestD d;

      public TestD D => d;

      public TestC()
      {
        Console.WriteLine("Instantiate TestC");
      }
    }

    public class TestD
    {
      public TestD()
      {
        Console.WriteLine("Instantiate TestD");
      }
    }

    public class TestE
    {
      public TestE()
      {
        Console.WriteLine("Instantiate TestE");
      }
    }

    [TestMethod]
    public void InjectWorks()
    {
      var builder = new AppContainerBuilder();

      builder.Register<TestC>()
        .For<TestA>();

      builder.Register<TestD>();

      builder.Register<TestE>()
        .For<TestB>();

      builder.Register<TestD>()
        .For<TestC>();

      builder.Register<TestA>()
        .ImplementedBy<TestB>();

      var a = builder.Container.Resolve<TestA>();
      Console.WriteLine(a.C == null);
    }
  }
}
