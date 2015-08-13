namespace EasyEventBus.Configuration
{
    public interface IInMemoryStrategyConfig
    {
        void SetContainer(IEventHandlerContainer container);
    }
}