using System;
using System.Collections.Generic;
using Ninject;

namespace ims.Configuration.Ninject
{
    using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            if (kernel == null)
            {
                throw new ArgumentNullException("kernel");
            }

            _kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            var service = _kernel.TryGet(serviceType);
            return service;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var services = _kernel.GetAll(serviceType);
            return services;
        }
    }

}