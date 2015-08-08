namespace EasyEventBus
{
    /// <summary>
    /// Represents an in-memory event bus.
    /// </summary>
    public sealed class InMemoryEventBus : EventBus
    {
        private readonly IEventHandlerContainer container;

        /// <summary>
        /// Creates a new instance of the InMemoryEventBus class.
        /// </summary>
        /// <param name="container">Instance of an event handler container.</param>
        public InMemoryEventBus(IEventHandlerContainer container)
        {
            this.container = container;
        }

        public override void Publish<T>(T eventData)
        {
            foreach (var handler in container.GetAll<T>())
            {
                handler.Handle(eventData);
            }
        }
    }
}
