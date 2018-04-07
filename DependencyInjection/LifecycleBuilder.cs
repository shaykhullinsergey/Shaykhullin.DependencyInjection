using System;

namespace DependencyInjection.Core
{
	internal class LifecycleBuilder : ILifecycleBuilder
	{
		private readonly Dependency dto;
		
		public LifecycleBuilder(Dependency dto)
		{
			this.dto = dto;
		}

		public void As<TLifecycle>() 
			where TLifecycle : ILifecycle
		{
			As(typeof(TLifecycle));
		}

		public void As(Type lifecycle)
		{
			dto.Lifecycle = lifecycle;
		}
	}
}