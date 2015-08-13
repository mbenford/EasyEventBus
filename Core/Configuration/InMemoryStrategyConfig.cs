using System;

namespace EasyEventBus.Configuration
{
    class InMemoryStrategyConfig : IInMemoryStrategyConfig
    {
        public void SetContainer(IEventHandlerContainer container)
        {
            if (Container != null) throw new InvalidOperationException("A handler container has already been set");
            Container = container;
        }

        public IEventHandlerContainer Container { get; private set; }
    }
}