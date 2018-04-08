using System;
using DependencyInjection.Core;

namespace DependencyInjection
{
	public interface IContainerConfig : IRegisterBuilder, IDisposable
	{
		IContainerConfig Scope();
		IContainer Container { get; }
	}
}