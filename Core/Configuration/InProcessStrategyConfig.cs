using System;
using System.Reflection;

namespace EasyEventBus.Configuration
{
    class InProcessStrategyConfig : IInProcessStrategyConfig
    {
        public InProcessStrategyConfig()
        {
            ServiceProvider = new DefaultServiceProvider();
        }

        public void SetAssemblies(Assembly[] assemblies)
        {
            Assemblies = assemblies;
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public Assembly[] Assemblies { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }
    }
}