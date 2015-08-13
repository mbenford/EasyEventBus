using System.Collections.Generic;

namespace EasyEventBus.Configuration
{
    class EventBusConfig : IEventBusConfig
    {
        private readonly IList<IPublicationStrategy> publicationStrategies = new List<IPublicationStrategy>();

        public void UsePublicationStrategy(IPublicationStrategy strategy)
        {
            publicationStrategies.Add(strategy);
        }

        public IEnumerable<IPublicationStrategy> PublicationStrategies
        {
            get { return publicationStrategies; }
        }
    }
}