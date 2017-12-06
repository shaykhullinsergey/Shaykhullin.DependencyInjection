using System;

namespace Inject
{
  public interface IDependency
  {
    Type Entity { get; }
    Type Implemented { get; set; }
    Type Dependency { get; set; }
    Func<IContainer, object> Factory { get; set; }
  }
}
