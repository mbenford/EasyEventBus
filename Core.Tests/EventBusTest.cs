using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace EasyEventBus.Tests
{
    public class EventBusTest
    {
        [Fact]
        public void Publishes_An_Event_To_All_Handlers_Synchronously()
        {
            // Arrange
            var containerMock = new Mock<IEventHandlerContainer>();
            var handlers = SetUpContainer<string>(containerMock, 3);

            var sut = new InMemoryEventBus(containerMock.Object);

            // Act
            sut.Publish("some data");

            // Assert
            AssertHandlersHaveBeenCalled(handlers, "some data");
        }

        [Fact]
        public async void Publishes_An_Event_To_All_Handlers_Asynchronously()
        {
            // Arrange
            var containerMock = new Mock<IEventHandlerContainer>();
            var handlers = SetUpContainer<int>(containerMock, 3);
            var sut = new InMemoryEventBus(containerMock.Object);

            // Act
            await sut.PublishAsync(42);

            // Assert
            AssertHandlersHaveBeenCalled(handlers, 42);
        }

        private Mock<IEventHandler<T>>[] SetUpContainer<T>(Mock<IEventHandlerContainer> container, int count)
        {
            var handlers = new List<Mock<IEventHandler<T>>>();
            for (int i = 0; i < count; i++) handlers.Add(new Mock<IEventHandler<T>>());
            container.Setup(c => c.GetAll<T>()).Returns(handlers.Select(h => h.Object));
            return handlers.ToArray();
        }

        private void AssertHandlersHaveBeenCalled<T>(Mock<IEventHandler<T>>[] handlers, T value)
        {
            foreach (var handler in handlers)
            {
                handler.Verify(h => h.Handle(value));
            }
        }
    }
}
