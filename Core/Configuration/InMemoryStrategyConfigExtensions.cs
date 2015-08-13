using System;
using System.Reflection;

namespace EasyEventBus.Configuration
{
    public static class InMemoryStrategyConfigExtensions
    {
        /// <summary>
        /// Configures an in-memory publication strategy.
        /// </summary>
        /// <param name="config">Configuration object.</param>
        /// <param name="setup">Configuration method.</param>
        public static void UseInMemoryStrategy(this IEventBusConfig config, Action<IInMemoryStrategyConfig> setup)
        {
            var containerConfiguration = new InMemoryStrategyConfig();
            setup(containerConfiguration);
            config.UsePublicationStrategy(new InMemoryPublicationStrategy(containerConfiguration.Container));
        }

        /// <summary>
        /// Tells the publication strategy to load event handlers from the provided assemblies.
        /// </summary>
        /// <param name="config">Configuration object.</param>
        /// <param name="assemblies">List of assemblies event handlers should be loaded from.</param>
        public static void LoadHandlersFromAssemblies(this IInMemoryStrategyConfig config, params Assembly[] assemblies)
        {
            config.SetContainer(new AssemblyHandlerContainer(assemblies));
        }

        /// <summary>
        /// Tells the publication strategy to load event handlers from the current assembly.
        /// </summary>
        /// <param name="config">Configuration object.</param>
        public static void LoadHandlersFromCurrentAssembly(this IInMemoryStrategyConfig config)
        {
            config.LoadHandlersFromAssemblies(Assembly.GetCallingAssembly());
        }
    }
}