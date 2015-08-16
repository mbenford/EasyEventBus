using System;
using Ninject;

namespace EasyEventBus.TypeResolvers.Ninject
{
    /// <summary>
    /// Resolves types by using Ninject.
    /// </summary>
    public class NinjectTypeResolver : ITypeResolver
    {
        private readonly IKernel kernel;

        public NinjectTypeResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public object Resolve(Type type)
        {
            return kernel.Get(type);
        }
    }
}
