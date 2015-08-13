namespace EasyEventBus.Configuration
{
    public interface IEventBusConfig
    {
        void UsePublicationStrategy(IPublicationStrategy strategy);
    }
}