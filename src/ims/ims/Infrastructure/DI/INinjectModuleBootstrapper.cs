using System.Collections.Generic;
using Ninject.Modules;

namespace ims.Infrastructure.DI
{
    public interface INinjectModuleBootstrapper
    {
        IList<INinjectModule> GetModules();
    }
}
