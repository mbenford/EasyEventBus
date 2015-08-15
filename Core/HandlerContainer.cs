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
        private readonly IDictionary<Type, IEnumerable<object>> cache;

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

            cache = new Dictionary<Type, IEnumerable<object>>();
        }

        public IEnumerable<IEventHandler<T>> GetAll<T>() where T : class
        {
            Type handlerType = typeof(IEventHandler<T>);
            if (!cache.ContainsKey(handlerType))
            {
                cache.Add(handlerType, LoadFromAssemblies(handlerType));
            }
            return cache[handlerType].Cast<IEventHandler<T>>();
        }

        private IEnumerable<object> LoadFromAssemblies(Type handlerType)
        {
            return from assembly in assemblies
                   from type in assembly.GetTypes()
                   where type != handlerType && handlerType.IsAssignableFrom(type)
                   select resolver.Resolve(type);
        }
    }
}