using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inject.Tests
{
  [TestClass]
  public class UnitTest1
  {
    class MyClass : IEquatable<MyClass>
    {
      public int Id;

      public override bool Equals(object obj)
      {
        return Equals(obj as MyClass);
      }

      public bool Equals(MyClass other)
      {
        return other != null &&
               Id == other.Id;
      }

      public override int GetHashCode()
      {
        return 2108858624 + Id.GetHashCode();
      }
    }

    [TestMethod]
    public void TestMethod1()
    {
      var hs = new HashSet<MyClass>();

      var a = new MyClass { Id = 1 };
      var b = new MyClass { Id = 2 };

      hs.Add(a);
      hs.Add(b);

      b.Id = 1;

      var c = new MyClass { Id = 2 };
      hs.Add(c);
    }
  }
}
