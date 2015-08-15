using System.Reflection;

namespace EasyEventBus.Configuration
{
    public interface IInMemoryStrategyConfig
    {
        void SetAssemblies(Assembly[] assemblies);
        void SetTypeResolver(ITypeResolver resolver);
    }
}