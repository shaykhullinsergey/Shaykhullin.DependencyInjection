using System;

namespace DependencyInjection.Core
{
	internal class ImplementedByBuilder<TRegister> : IImplementedByBuilder<TRegister>
		where TRegister : class
	{
		private readonly Dependency dto;
		
		public ImplementedByBuilder(Dependency dto)
		{
			this.dto = dto;
		}

		public ILifecycleBuilder ImplementedBy<TImplemented>(Func<IContainer, TImplemented> factory = null) 
			where TImplemented : TRegister
		{
			return ImplementedBy(typeof(TImplemented), 
				factory == null 
					? default(Func<IContainer, object>)
					: c => factory(c));
		}

		public ILifecycleBuilder ImplementedBy(Type implemented, Func<IContainer, object> factory = null)
		{
			if (factory != null)
			{
				dto.Factory = container => factory(container);
			}

			dto.Implemented = implemented;
			
			return new LifecycleBuilder(dto);
		}
	}
}