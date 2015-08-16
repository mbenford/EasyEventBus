using System.Reflection;

namespace EasyEventBus.Configuration
{
    class InProcessStrategyConfig : IInProcessStrategyConfig
    {
        public InProcessStrategyConfig()
        {
            Resolver = new DefaultTypeResolver();
        }

        public void SetAssemblies(Assembly[] assemblies)
        {
            Assemblies = assemblies;
        }

        public void SetTypeResolver(ITypeResolver resolver)
        {
            Resolver = resolver;
        }

        public Assembly[] Assemblies { get; private set; }
        public ITypeResolver Resolver { get; private set; }
    }
}