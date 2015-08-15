using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasyEventBus.Util;

namespace EasyEventBus
{
    /// <summary>
    /// Scans an assembly and loads all declared event handlers.
    /// </summary>
    public sealed class HandlerContainer : IEventHandlerContainer
    {
        private readonly ITypeResolver resolver;
        private readonly Assembly[] assemblies;

        /// <summary>
        /// Creates a new instance of the HandlerContainer class and scans the provided assemblies.
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="assemblies">Assemblies to be scanned for event handlers.</param>
        public HandlerContainer(ITypeResolver resolver, params Assembly[] assemblies)
        {
            Precondition.NotNull(resolver);
            Precondition.NotNull(assemblies);
            Precondition.NotEmpty(assemblies);

            this.resolver = resolver;
            this.assemblies = assemblies;
        }

        public IEnumerable<IEventHandler<T>> GetAll<T>() where T : class
        {
            Type eventHandlerType = typeof(IEventHandler<T>);
            return from assembly in assemblies
                   from type in assembly.GetTypes()
                   where type != eventHandlerType && eventHandlerType.IsAssignableFrom(type)
                   select (IEventHandler<T>)resolver.Resolve(type);
        }
    }
}