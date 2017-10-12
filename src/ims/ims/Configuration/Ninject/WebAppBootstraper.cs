using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ims.Infrastructure.DI;
using Ninject.Modules;

namespace ims.Configuration.Ninject
{
    public class WebAppBootstraper : INinjectModuleBootstrapper
    {
        public IList<INinjectModule> GetModules()
        {
            return new List<INinjectModule>
            {
                new WebNinjectModule()
            };
        }
    }
}