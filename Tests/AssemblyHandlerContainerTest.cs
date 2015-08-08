using System.Linq;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace EasyEventBus.Tests
{
    public class AssemblyHandlerContainerTest
    {
        [Fact]
        public void Loads_All_Event_Handlers_From_The_Executing_Assembly()
        {
            // Arrange
            var sut = new AssemblyHandlerContainer();

            // Act/Assert
            using (new AssertionScope())
            {
                sut.GetAll<Foo>()
                   .Select(e => e.GetType())
                   .Should().BeEquivalentTo(typeof(FooHandler), typeof(BarHandler));

                sut.GetAll<Bar>()
                   .Select(e => e.GetType())
                   .Should().BeEquivalentTo(typeof(FooHandler), typeof(BarHandler));
            }
        }

        [Fact]
        public void Loads_All_Event_Handlers_From_The_Provided_Assembly()
        {
            // Arrange
            var sut = new AssemblyHandlerContainer(Assembly.GetExecutingAssembly());

            // Act/Assert
            using (new AssertionScope())
            {
                sut.GetAll<Foo>()
                   .Select(e => e.GetType())
                   .Should().BeEquivalentTo(typeof(FooHandler), typeof(BarHandler));

                sut.GetAll<Bar>()
                   .Select(e => e.GetType())
                   .Should().BeEquivalentTo(typeof(FooHandler), typeof(BarHandler));
            }
        }

        [Fact]
        public void Loads_All_Event_Handlers_From_The_Provided_Assemblies()
        {
            // Arrange
            var sut = new AssemblyHandlerContainer(new[] {
                Assembly.GetExecutingAssembly(),
                Assembly.GetExecutingAssembly()
            });

            // Act/Assert
            using (new AssertionScope())
            {
                sut.GetAll<Foo>()
                   .Select(e => e.GetType())
                   .Should().BeEquivalentTo(typeof(FooHandler), typeof(BarHandler), typeof(FooHandler), typeof(BarHandler));

                sut.GetAll<Bar>()
                   .Select(e => e.GetType())
                   .Should().BeEquivalentTo(typeof(FooHandler), typeof(BarHandler), typeof(FooHandler), typeof(BarHandler));
            }
        }

        class Foo { }
        class Bar : Foo { }

        class FooHandler : IEventHandler<Foo>
        {
            public void Handle(Foo eventData) { }
        }

        class BarHandler : FooHandler, IEventHandler<Bar>
        {
            public void Handle(Bar eventData) { }
        }
    }
}