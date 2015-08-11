using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyEventBus.Util;

namespace EasyEventBus
{
    /// <summary>
    /// Represents an event bus.
    /// </summary>
    public sealed class EventBus
    {
        private readonly IEnumerable<IPublicationStrategy> publicationStrategies;

        /// <summary>
        /// Creates a new instance of the EventBus class.
        /// </summary>
        /// <param name="publicationStrategy">Publication strategy to be used for publishing events.</param>
        public EventBus(IPublicationStrategy publicationStrategy)
        {
            Precondition.NotNull(publicationStrategy);

            publicationStrategies = new[] { publicationStrategy };
        }

        /// <summary>
        /// Creates a new instance of the EventBus class.
        /// </summary>
        /// <param name="publicationStrategies">List of publication strategies to be used for publishing events.</param>
        public EventBus(IEnumerable<IPublicationStrategy> publicationStrategies)
            : this(publicationStrategies.ToArray())
        {
        }

        private EventBus(IPublicationStrategy[] publicationStrategies)
        {
            Precondition.NotNull(publicationStrategies);
            Precondition.NotEmpty(publicationStrategies);

            this.publicationStrategies = publicationStrategies;
        }

        /// <summary>
        /// Publishes an event to registered handlers of the provided type.
        /// </summary>
        /// <typeparam name="T">Type of the event. Must be a reference type.</typeparam>
        /// <param name="eventData">Data of the event.</param>
        public void Publish<T>(T eventData) where T : class
        {
            Precondition.NotNull(eventData);

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
            Precondition.NotNull(eventData);

            await Task.Factory.StartNew(() => Publish(eventData));
        }
    }
}