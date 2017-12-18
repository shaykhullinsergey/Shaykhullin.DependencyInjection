using Autofac;
using Autofac.Builder;
using System;
using System.Diagnostics;

namespace Inject.Sandbox
{
  class A
  {
    public int Field;
  }

  class B : A
  {
    [Inject, Shaykhullin.Injection.Inject]
    private C c;

    public C C { get; set; }
  }

  class C
  {
    public int X;
  }

  class AA
  {
    [Inject]
    private BB b;
  }

  class BB
  {
    [Inject]
    private CC c;
  }

  class CC
  {
    [Inject]
    private DD dd;
  }

  class DD
  {
    [Inject]
    private EE e;
  }

  class EE
  {
    [Inject]
    private FF f;
  }

  class FF
  {

  }

  class Program
  {
    private static int count = 100000;

    static void Main(string[] args)
    {
      var builder = new AppContainerBuilder();

      builder.Register<AA>();
      builder.Register<BB>();
      builder.Register<CC>();
      builder.Register<EE>();
      builder.Register<DD>();
      builder.Register<FF>();

      var container = builder.Container;

      var a = container.Resolve<AA>();
    }

    static void TestSingletonAndTransient()
    {
      var cb = new ContainerBuilder();
      cb.RegisterType<B>()
        .PropertiesAutowired();

      cb.RegisterType<C>()
        .InstancePerDependency();

      var container = cb.Build();

      var b1 = container.Resolve<B>();
      var b2 = container.Resolve<B>();

      Console.WriteLine(b1.C);
      Console.WriteLine(b1.C == b2.C);
    }

    static void Benchmark()
    {
      var f = Stopwatch.Frequency;
      Autofac();
      Inject();
      Shaykhullin();

      GC.Collect();
      GC.WaitForPendingFinalizers();
      GC.Collect();

      var sw = Stopwatch.StartNew();
      Inject();
      Console.WriteLine(sw.ElapsedMilliseconds);

      GC.Collect();
      GC.WaitForPendingFinalizers();
      GC.Collect();

      sw.Restart();
      Autofac();
      Console.WriteLine(sw.ElapsedMilliseconds);

      GC.Collect();
      GC.WaitForPendingFinalizers();
      GC.Collect();

      sw.Restart();
      Shaykhullin();
      Console.WriteLine(sw.ElapsedMilliseconds);
    }

    static A[] Shaykhullin()
    {
      var builder = new Shaykhullin.Injection.AppContainerBuilder();

      var container = builder.Register<B>()
        .As<A>()
        .Register<C>()
        .Container;

      var arr = new A[count];

      for (int i = 0; i < count; i++)
      {
        arr[i] = container.Resolve<A>();
      }

      return arr;
    }

    static A[] Autofac()
    {
      Console.WriteLine("Autofac");
      var sw = Stopwatch.StartNew();

      var builder = new ContainerBuilder();

      builder.RegisterType<B>().As<A>()
        .InstancePerDependency()
        .PropertiesAutowired();

      builder.RegisterType<C>()
        .SingleInstance()
        .PropertiesAutowired();

      var arr = new A[count];

      Console.WriteLine(sw.ElapsedTicks);
      sw.Restart();

      using (var container = builder.Build())
      {
        for (int i = 0; i < count; i++)
        {
          arr[i] = container.Resolve<A>();
        }
      }

      Console.WriteLine(sw.ElapsedMilliseconds);

      return arr;
    }

    static A[] Inject()
    {
      Console.WriteLine("Inject");
      var sw = Stopwatch.StartNew();

      var builder = new AppContainerBuilder();
      builder.Register<A>()
        .ImplementedBy<B>()
        .Returns(c => new B())
        .As<Singleton>();

      builder.Register<C>()
        .Returns(c => new C())
        .For<B>();

      var arr = new A[count];

      Console.WriteLine(sw.ElapsedTicks);

      sw.Restart();

      var container = builder.Container;

      for (int i = 0; i < count; i++)
      {
        arr[i] = container.Resolve<A>();
      }

      Console.WriteLine(sw.ElapsedMilliseconds);

      return arr;
    }
  }
}
