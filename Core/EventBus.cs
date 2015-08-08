using System.Threading.Tasks;

namespace EasyEventBus
{
    /// <summary>
    /// Represents an event bus.
    /// </summary>
    public abstract class EventBus
    {
        /// <summary>
        /// Publishes an event to registered handlers of the provided type.
        /// </summary>
        /// <typeparam name="T">Type of the event.</typeparam>
        /// <param name="eventData">Data of the event.</param>
        public abstract void Publish<T>(T eventData);

        /// <summary>
        /// Publishes an event to registered handlers of the provided type asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of the event.</typeparam>
        /// <param name="eventData">Data of the event.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task PublishAsync<T>(T eventData)
        {
            await Task.Factory.StartNew(() => Publish(eventData));
        }
    }
}