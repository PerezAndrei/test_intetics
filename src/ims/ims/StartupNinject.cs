using Ninject;
using System;
using Ninject.Web.Common.OwinHost;
using Owin;
using ims.Configuration.Ninject;

namespace ims
{
    public partial class Startup
    {
        public IKernel ConfigureNinject(IAppBuilder app)
        {
            //var config = GlobalConfiguration.Configuration;
            var kernel = CreateKernel();
            app.UseNinjectMiddleware(() => kernel);//.UseNinjectWebApi(config);

            return kernel;
        }

        public IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            //var binder = new AutoMapperBinder();
            //var signalRBinder = new SignalRBinder();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            kernel.LoadAssemblies(assemblies);
            kernel.LoadMappers(assemblies);
            //kernel.Load(Assembly.GetExecutingAssembly());

            //binder.Register(kernel);
            //signalRBinder.Register(kernel);
            return kernel;
        }

    }
}