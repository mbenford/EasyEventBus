using System;
using System.Linq;
using Moq;
using Xunit;

namespace EasyEventBus.Tests
{
    public class EventBusTest
    {
        [Fact]
        public void Throws_An_Exception_If_A_Null_Publication_Strategy_Is_Provided()
        {
            Assert.Throws<ArgumentNullException>(() => new EventBus((IPublicationStrategy)null));
        }

        [Fact]
        public void Throws_An_Exception_If_An_Empty_Publication_Strategy_List_Is_Provided()
        {
            Assert.Throws<ArgumentException>(() => new EventBus(new IPublicationStrategy[] { }));
        }

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

        [Fact]
        public void Throws_An_Exception_If_A_Null_Event_Data_Is_Provided_Synchronously()
        {
            // Arrange
            var publicationStrategyMock = new Mock<IPublicationStrategy>();
            var sut = new EventBus(publicationStrategyMock.Object);

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => sut.Publish((object)null));
        }

        [Fact]
        public async void Throws_An_Exception_If_A_Null_Event_Data_Is_Provided_Asynchronously()
        {
            // Arrange
            var publicationStrategyMock = new Mock<IPublicationStrategy>();
            var sut = new EventBus(publicationStrategyMock.Object);

            // Act/Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.PublishAsync((object)null));
        }
    }
}
