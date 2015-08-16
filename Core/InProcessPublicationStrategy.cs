using EasyEventBus.Util;

namespace EasyEventBus
{
    /// <summary>
    /// Represents an in-process event bus.
    /// </summary>
    public sealed class InProcessPublicationStrategy : IPublicationStrategy
    {
        private readonly IEventHandlerContainer container;

        /// <summary>
        /// Creates a new instance of the InProcessPublicationStrategy class.
        /// </summary>
        /// <param name="container">Instance of an event handler container.</param>
        public InProcessPublicationStrategy(IEventHandlerContainer container)
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
