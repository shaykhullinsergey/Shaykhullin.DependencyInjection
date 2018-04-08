using System;
using System.Collections.Generic;
using DependencyInjection.Core;

namespace DependencyInjection
{
	internal class DependencyContainer
	{
		private readonly DependencyContainer parent;
		private readonly IList<Dependency> dependencies;

		public DependencyContainer(DependencyContainer parent)
		{
			this.parent = parent;
			dependencies = new List<Dependency>();
		}

		public Dependency Register(Type register)
		{
			var dto = new Dependency(register);
			dependencies.Add(dto);
			return dto;
		}

		public Dependency Get(Type register, Type @for = null)
		{
			if (@for == null)
			{
				for (var i = 0; i < dependencies.Count; i++)
				{
					var dependency = dependencies[i];

					if (dependency.Register == register && dependency.For == null)
					{
						return dependency;
					}
				}
			}
			else
			{
				Dependency forNullFor = null;

				for (var i = 0; i < dependencies.Count; i++)
				{
					var dependency = dependencies[i];

					if (dependency.Register == register && dependency.For == @for)
					{
						return dependency;
					}

					if (dependency.Register == register && dependency.For == null)
					{
						forNullFor = dependency;
					}
				}

				if (forNullFor != null)
				{
					return forNullFor;
				}
			}

			return parent?.Get(register, @for);
		}
	}
}