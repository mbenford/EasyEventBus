using System.Collections.Generic;

namespace EasyEventBus
{
    /// <summary>
    /// Represents a container of event handlers.
    /// </summary>
    public interface IEventHandlerContainer
    {
        /// <summary>
        /// Gets all event handlers of the provided type.
        /// </summary>
        /// <typeparam name="T">Type of the event. Must be a reference type.</typeparam>
        /// <returns>A list of event handlers.</returns>
        IEnumerable<IEventHandler<T>> GetAll<T>() where T : class;
    }
}