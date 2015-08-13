using System;

namespace EasyEventBus.Configuration
{
    /// <summary>
    /// Creates an event bus
    /// </summary>
    public static class EventBusFactory
    {
        /// <summary>
        /// Creates an event bus.
        /// </summary>
        /// <param name="setup"></param>
        /// <returns>An event bus.</returns>
        public static IEventBus Create(Action<IEventBusConfig> setup)
        {
            var configuration = new EventBusConfig();
            setup(configuration);
            return new EventBus(configuration.PublicationStrategies);
        }

        /// <summary>
        /// Creates a typed event bus.
        /// </summary>
        /// <typeparam name="T">Type to be used as a constraint for publishing events.</typeparam>
        /// <param name="setup"></param>
        /// <returns></returns>
        public static IEventBus<T> Create<T>(Action<IEventBusConfig> setup) where T : class
        {
            return Create(setup).As<T>();
        }
    }
}