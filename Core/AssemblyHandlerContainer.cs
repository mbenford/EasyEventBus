using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasyEventBus
{
    /// <summary>
    /// Scans an assembly and loads all declared event handlers.
    /// </summary>
    public sealed class AssemblyHandlerContainer : IEventHandlerContainer
    {
        private readonly IEnumerable<Assembly> assemblies;

        /// <summary>
        /// Creates a new instance of the AssemblyHandlerContainer class and scans the provided assemblies.
        /// </summary>
        /// <param name="assemblies">Assemblies to be scanned for event handlers.</param>
        public AssemblyHandlerContainer(IEnumerable<Assembly> assemblies)
        {
            this.assemblies = assemblies;
        }

        /// <summary>
        /// Creates a new instance of the AssemblyHandlerContainer class and scans the provided assembly.
        /// </summary>
        /// <param name="assembly">Assembly to be scanned for event handlers.</param>
        public AssemblyHandlerContainer(Assembly assembly)
            : this(new[] { assembly })
        {
        }

        /// <summary>
        /// Creates a new instance of the AssemblyHandlerContainer class and scans the executing assembly.
        /// </summary>
        public AssemblyHandlerContainer()
            : this(Assembly.GetCallingAssembly())
        {
        }

        public IEnumerable<IEventHandler<T>> GetAll<T>()
        {
            Type eventHandlerType = typeof(IEventHandler<T>);
            return from assembly in assemblies
                   from type in assembly.GetTypes()
                   where type != eventHandlerType && eventHandlerType.IsAssignableFrom(type)
                   select (IEventHandler<T>)Activator.CreateInstance(type);
        }
    }
}