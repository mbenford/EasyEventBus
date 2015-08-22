using System;
using System.Reflection;

namespace EasyEventBus.Configuration
{
    public interface IInProcessStrategyConfig
    {
        void SetAssemblies(Assembly[] assemblies);
        void SetServiceProvider(IServiceProvider serviceProvider);
    }
}