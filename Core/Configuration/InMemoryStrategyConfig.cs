using System.Reflection;

namespace EasyEventBus.Configuration
{
    class InMemoryStrategyConfig : IInMemoryStrategyConfig
    {
        public InMemoryStrategyConfig()
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