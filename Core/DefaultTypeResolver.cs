using System;

namespace EasyEventBus
{
    /// <summary>
    /// Resolves types having parameterless constructors.
    /// </summary>
    public sealed class DefaultTypeResolver : ITypeResolver
    {
        public object Resolve(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}