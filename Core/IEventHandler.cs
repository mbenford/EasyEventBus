namespace EasyEventBus
{
    /// <summary>
    /// Represents an event handler of the declared type.
    /// </summary>
    /// <typeparam name="T">Type of the event that the handler should process.</typeparam>
    public interface IEventHandler<in T> where T : class
    {
        /// <summary>
        /// Process an event of the declared type.
        /// </summary>
        /// <param name="eventData">Data of the event.</param>
        void Handle(T eventData);
    }
}