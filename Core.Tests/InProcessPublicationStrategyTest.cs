using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

namespace EasyEventBus.Tests
{
    public class InProcessPublicationStrategyTest
    {
        [Theory]
        [InlineData("some data", 1)]
        [InlineData("some other data", 2)]
        [InlineData("yet another data", 3)]
        public void Publishes_An_Event_To_All_Handlers<T>(T data, int count) where T : class
        {
            // Arrange
            var containerMock = new Mock<IEventHandlerContainer>();
            var handlers = SetUpContainer<T>(containerMock, count);

            var sut = new InProcessPublicationStrategy(containerMock.Object);

            // Act
            sut.Publish(data);

            // Assert
            AssertHandlersHaveBeenCalled(handlers, data);
        }

        [Fact]
        public void Throws_An_Exception_If_A_Null_Container_Is_Provided()
        {
            Assert.Throws<ArgumentNullException>(() => new InProcessPublicationStrategy(null));
        }

        [Fact]
        public void Throws_An_Exception_If_A_Null_Event_Data_Is_Provided()
        {
            // Arrange
            var containerMock = new Mock<IEventHandlerContainer>();
            var sut = new InProcessPublicationStrategy(containerMock.Object);

            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => sut.Publish((object)null));
        }

        private Mock<IEventHandler<T>>[] SetUpContainer<T>(Mock<IEventHandlerContainer> container, int count) where T : class
        {
            var handlers = new List<Mock<IEventHandler<T>>>();
            for (int i = 0; i < count; i++) handlers.Add(new Mock<IEventHandler<T>>());
            container.Setup(c => c.GetAll<T>()).Returns(handlers.Select(h => h.Object));
            return handlers.ToArray();
        }

        private void AssertHandlersHaveBeenCalled<T>(Mock<IEventHandler<T>>[] handlers, T value) where T : class
        {
            foreach (var handler in handlers)
            {
                handler.Verify(h => h.Handle(value));
            }
        }
    }
}
