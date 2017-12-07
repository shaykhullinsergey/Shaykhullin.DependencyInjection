using System;
using System.Collections.Generic;
using System.Reflection;

namespace Inject
{
	interface IDependencyInfo : IDependencyMetaInfo
	{
		ILifeTime LifeTime { get; set; }
		IEnumerable<FieldInfo> HierarchicalInjectFields { get; }
		IEnumerable<FieldInfo> InjectFields { get; }
	}
}
