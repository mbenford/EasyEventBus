using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace EasyEventBus.Tests
{
    public class HandlerContainerTest
    {
        [Fact]
        public void Throws_An_Exception_If_A_Null_Handler_Resolver_Is_Provided()
        {
            Assert.Throws<ArgumentNullException>(() => new HandlerContainer(null));
        }

        [Fact]
        public void Throws_An_Exception_If_An_Empty_Assembly_List_Is_Provided()
        {
            Assert.Throws<ArgumentException>(() => new HandlerContainer(new DefaultTypeResolver()));
        }

        [Fact]
        public void Throws_An_Exception_If_A_Null_Assembly_List_Is_Provided()
        {
            Assert.Throws<ArgumentNullException>(() => new HandlerContainer(new DefaultTypeResolver(), null));
        }

        [Fact]
        public void Loads_All_Event_Handlers_From_The_Provided_Assemblies()
        {
            // Arrange
            var sut = new HandlerContainer(new DefaultTypeResolver(), Assembly.GetExecutingAssembly(), Assembly.GetExecutingAssembly());

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