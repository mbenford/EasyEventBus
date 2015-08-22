using System;

namespace EasyEventBus
{
    /// <summary>
    /// Resolves types having parameterless constructors.
    /// </summary>
    public sealed class DefaultServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return Activator.CreateInstance(serviceType);
        }
    }
}