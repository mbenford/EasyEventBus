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
        private readonly IDictionary<Type, Type[]> types;

        /// <summary>
        /// Creates a new instance of the HandlerContainer class and scans the provided assemblies.
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="assemblies">Assemblies to be scanned for event handlers.</param>
        public HandlerContainer(ITypeResolver resolver, params Assembly[] assemblies)
        {
            Precondition.NotNull(resolver);
            Precondition.NotEmpty(assemblies);

            this.resolver = resolver;
            this.assemblies = assemblies;

            types = new Dictionary<Type, Type[]>();
        }

        public IEnumerable<IEventHandler<T>> GetAll<T>() where T : class
        {
            return GetTypes(typeof(IEventHandler<T>))
                  .Select(type => resolver.Resolve(type))
                  .Cast<IEventHandler<T>>();
        }

        public Type[] GetTypes(Type type)
        {
            if (!types.ContainsKey(type))
            {
                types.Add(type, LoadFromAssemblies(type));
            }
            return types[type];
        }

        private Type[] LoadFromAssemblies(Type handlerType)
        {
            return assemblies.SelectMany(assembly => assembly.GetTypes(), (assembly, type) => type)
                             .Where(type => type != handlerType && handlerType.IsAssignableFrom(type))
                             .Select(type => type)
                             .ToArray();
        }
    }
}