using System.Reflection;
using System.Collections.Generic;

namespace Inject
{
  interface IDependencyInfo : IDependencyMetaInfo
	{
		ILifeTime LifeTime { get; set; }

		IEnumerable<FieldInfo> InjectFields { get; }
		IEnumerable<FieldInfo> HierarchicalInjectFields { get; }
	}
}
