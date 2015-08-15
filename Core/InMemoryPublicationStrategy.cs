using EasyEventBus.Util;

namespace EasyEventBus
{
    /// <summary>
    /// Represents an in-memory event bus.
    /// </summary>
    public sealed class InMemoryPublicationStrategy : IPublicationStrategy
    {
        private readonly IEventHandlerContainer container;

        /// <summary>
        /// Creates a new instance of the InMemoryPublicationStrategy class.
        /// </summary>
        /// <param name="container">Instance of an event handler container.</param>
        public InMemoryPublicationStrategy(IEventHandlerContainer container)
        {
            Precondition.NotNull(container);

            this.container = container;
        }

        public void Publish<T>(T eventData) where T : class
        {
            Precondition.NotNull(eventData);

            foreach (var handler in container.GetAll<T>())
            {
                handler.Handle(eventData);
            }
        }
    }
}
