using System;
using System.Reflection;

namespace EasyEventBus.Configuration
{
    public static class InProcessStrategyConfigExtensions
    {
        /// <summary>
        /// Configures an in-memory publication strategy.
        /// </summary>
        /// <param name="config">Configuration object.</param>
        /// <param name="setup">Configuration method.</param>
        public static void UseInProcessStrategy(this IEventBusConfig config, Action<IInProcessStrategyConfig> setup)
        {
            var strategyConfig = new InProcessStrategyConfig();
            setup(strategyConfig);

            var handlerContainer = new HandlerContainer(strategyConfig.ServiceProvider, strategyConfig.Assemblies);
            config.UsePublicationStrategy(new InProcessPublicationStrategy(handlerContainer));
        }

        /// <summary>
        /// Tells the publication strategy to load event handlers from the provided assemblies.
        /// </summary>
        /// <param name="config">Configuration object.</param>
        /// <param name="assemblies">List of assemblies event handlers should be loaded from.</param>
        public static void LoadHandlersFromAssemblies(this IInProcessStrategyConfig config, params Assembly[] assemblies)
        {
            config.SetAssemblies(assemblies);
        }

        /// <summary>
        /// Tells the publication strategy to load event handlers from the current assembly.
        /// </summary>
        /// <param name="config">Configuration object.</param>
        public static void LoadHandlersFromCurrentAssembly(this IInProcessStrategyConfig config)
        {
            config.LoadHandlersFromAssemblies(Assembly.GetCallingAssembly());
        }

        public static void UseServiceProvider(this IInProcessStrategyConfig config, IServiceProvider serviceProvider)
        {
            config.SetServiceProvider(serviceProvider);
        }
    }
}