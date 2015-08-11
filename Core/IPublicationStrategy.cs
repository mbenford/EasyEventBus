namespace EasyEventBus
{
    /// <summary>
    /// Represents a publication strategy.
    /// </summary>
    public interface IPublicationStrategy
    {
        /// <summary>
        /// Publishes an event.
        /// </summary>
        /// <typeparam name="T">Type of the event. Must be a reference type.</typeparam>
        /// <param name="eventData">Data of the event.</param>
        void Publish<T>(T eventData) where T : class;
    }
}