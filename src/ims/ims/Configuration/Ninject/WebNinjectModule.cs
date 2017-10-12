using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Ninject.Web.WebApi;

namespace ims.Configuration.Ninject
{
    public class WebNinjectModule : NinjectModule

    {
        public override void Load()
        {
            RegisterServices(KernelInstance);
        }
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Bind<IOAuthAuthorizationServerOptions>().To<EdiaryOAuthAuthorizationServerOptions>();
            //kernel.Bind<IOAuthAuthorizationServerProvider>().To<AuthorizationServerProvider>();
            //kernel.Bind<IAuthenticationTokenProvider>().To<RefreshTokenProvider>();

            //kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            //kernel.Bind<IRepository>().To<EntityFrameworkRepository<ApplicationDbContext>>().InRequestScope();
            

            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));



            //global init
            //kernel.Bind<IAccountService>().To<AccountService>();
            //kernel.Bind<IRefreshTokenClientService>().To<RefreshTokenClientService>();

        }
    }
}
