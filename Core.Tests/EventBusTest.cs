using System.Linq;
using Moq;
using Xunit;

namespace EasyEventBus.Tests
{
    public class EventBusTest
    {
        [Fact]
        public void Uses_The_Provided_Strategies_To_Publish_An_Event_Synchronously()
        {
            // Arrange
            var publicationStrategyMock = new[]
            {
                new Mock<IPublicationStrategy>(),
                new Mock<IPublicationStrategy>(),
                new Mock<IPublicationStrategy>()
            };
            var sut = new EventBus(publicationStrategyMock.Select(m => m.Object));

            // Act
            sut.Publish("some data");

            // Assert
            publicationStrategyMock[0].Verify(s => s.Publish("some data"));
            publicationStrategyMock[1].Verify(s => s.Publish("some data"));
            publicationStrategyMock[2].Verify(s => s.Publish("some data"));
        }

        [Fact]
        public async void Uses_The_Provided_Strategies_To_Publish_An_Event_Asynchronously()
        {
            // Arrange
            var publicationStrategyMock = new[]
            {
                new Mock<IPublicationStrategy>(),
                new Mock<IPublicationStrategy>(),
                new Mock<IPublicationStrategy>()
            };
            var sut = new EventBus(publicationStrategyMock.Select(m => m.Object));

            // Act
            await sut.PublishAsync("some data");

            // Assert
            publicationStrategyMock[0].Verify(s => s.Publish("some data"));
            publicationStrategyMock[1].Verify(s => s.Publish("some data"));
            publicationStrategyMock[2].Verify(s => s.Publish("some data"));
        }
    }
}
