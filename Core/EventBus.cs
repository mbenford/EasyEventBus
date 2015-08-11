using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyEventBus
{
    /// <summary>
    /// Represents an event bus.
    /// </summary>
    public sealed class EventBus
    {
        private readonly IEnumerable<IPublicationStrategy> publicationStrategies;

        public EventBus(IPublicationStrategy publicationStrategy)
            : this(new[] { publicationStrategy })
        {
        }

        public EventBus(IEnumerable<IPublicationStrategy> publicationStrategies)
        {
            if (publicationStrategies == null)
                throw new ArgumentNullException("publicationStrategies");
            this.publicationStrategies = publicationStrategies;
        }

        /// <summary>
        /// Publishes an event to registered handlers of the provided type.
        /// </summary>
        /// <typeparam name="T">Type of the event. Must be a reference type.</typeparam>
        /// <param name="eventData">Data of the event.</param>
        public void Publish<T>(T eventData) where T : class
        {
            foreach (var strategy in publicationStrategies)
            {
                strategy.Publish(eventData);
            }
        }

        /// <summary>
        /// Publishes an event to registered handlers of the provided type asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of the event. Must be a reference type.</typeparam>
        /// <param name="eventData">Data of the event.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task PublishAsync<T>(T eventData) where T : class
        {
            await Task.Factory.StartNew(() => Publish(eventData));
        }
    }
}