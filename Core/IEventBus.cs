using System.Threading.Tasks;

namespace EasyEventBus
{
    /// <summary>
    /// Represents an event bus.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Publishes an event to registered handlers of the provided type.
        /// </summary>
        /// <typeparam name="T">Type of the event. Must be a reference type.</typeparam>
        /// <param name="eventData">Data of the event.</param>
        void Publish<T>(T eventData) where T : class;

        /// <summary>
        /// Publishes an event to registered handlers of the provided type asynchronously.
        /// </summary>
        /// <typeparam name="T">Type of the event. Must be a reference type.</typeparam>
        /// <param name="eventData">Data of the event.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/>.</returns>
        Task PublishAsync<T>(T eventData) where T : class;

        /// <summary>
        /// Creates a typed event bus.
        /// </summary>
        /// <typeparam name="T">Type to be used as a constraint for publishing events.</typeparam>
        /// <returns>An implementation of the <see cref="IEventBus{T}"/> interface</returns>
        IEventBus<T> As<T>() where T : class;
    }

    /// <summary>
    /// Represents a typed event bus.
    /// </summary>
    /// <typeparam name="T">Type of the event. Must be a reference type.</typeparam>
    public interface IEventBus<in T> where T : class
    {
        /// <summary>
        /// Publishes an event to registered handlers of the provided type.
        /// </summary>
        /// <param name="eventData">Data of the event.</param>
        void Publish(T eventData);

        /// <summary>
        /// Publishes an event to registered handlers of the provided type asynchronously.
        /// </summary>
        /// <param name="eventData">Data of the event.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/>.</returns>
        Task PublishAsync(T eventData);
    }
}