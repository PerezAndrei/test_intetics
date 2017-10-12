using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(ims.Startup))]

namespace ims
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            var kernel = ConfigureNinject(app);
            ConfigureAuth(app, kernel);
        }
        
        public void ConfigureAuth(IAppBuilder app, IKernel kernel)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/LogIn")
            });
        }
    }
}
