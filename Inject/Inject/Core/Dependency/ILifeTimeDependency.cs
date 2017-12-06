using System;

namespace Inject
{
	interface ILifeTimeDependency : IDependency
	{
		ILifeTime LifeTime { get; set; }
	}
}
