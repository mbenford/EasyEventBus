using System;

namespace EasyEventBus
{
    /// <summary>
    /// Represents an event handler resolver.
    /// </summary>
    public interface ITypeResolver
    {
        /// <summary>
        /// Returns an instance of the provided type.
        /// </summary>
        /// <param name="type">Type to be returned.</param>
        /// <returns>An instance of the provided type.</returns>
        object Resolve(Type type);
    }
}