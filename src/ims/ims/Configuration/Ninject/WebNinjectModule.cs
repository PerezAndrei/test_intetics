using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ims.DataAccess.Models;
using ims.DataAccess.Repository;
using ims.Domain.IServices;
using ims.Domain.Services;
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
            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            kernel.Bind<IRepository>().To<EntityFrameworkRepository<ApplicationDbContext>>().InRequestScope();
            
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            
            //global init
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IImageService>().To<ImageService>();
            kernel.Bind<ITagService>().To<TagService>();

        }
    }
}
