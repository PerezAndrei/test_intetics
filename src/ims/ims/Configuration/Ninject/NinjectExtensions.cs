using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using AutoMapper;
using ims.Infrastructure.DI;
using ims.Infrastructure.Mapping;
using Ninject;

namespace ims.Configuration.Ninject
{
    public static class NinjectExtensions
    {
        public static void LoadAssemblies(this StandardKernel standardKernel, IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                assembly
                    .GetTypes()
                    .Where(t =>
                        t.GetInterfaces()
                            .Any(i =>
                                i.Name == typeof(INinjectModuleBootstrapper).Name))
                    .ToList()
                    .ForEach(t =>
                    {
                        var ninjectModuleBootstrapper =
                            (INinjectModuleBootstrapper)Activator.CreateInstance(t);

                        standardKernel.Load(ninjectModuleBootstrapper.GetModules());
                    });
            }
        }

        public static void LoadMappers(this StandardKernel standardKernel, IEnumerable<Assembly> assemblies)
        {
            var profiles = new List<Profile>();
            foreach (var assembly in assemblies)
            {
                assembly
                    .GetTypes()
                    .Where(t =>
                        t.GetInterfaces()
                            .Any(i =>
                                i.Name == typeof(IProfileBootstrapper).Name))
                    .ToList()
                    .ForEach(t =>
                    {
                        var profileBootstrapper = (IProfileBootstrapper)Activator.CreateInstance(t);
                        profiles.AddRange(profileBootstrapper.GetProfiles());
                    });
            }

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
                cfg.ForAllMaps((map, expresion) => expresion.ForAllMembers(options => options.Condition((source, destination, member) => member != null)));
            });

            var mapper = config.CreateMapper();
            standardKernel.Bind<MapperConfiguration>().ToConstant(config).InSingletonScope();
            standardKernel.Bind<IMapper>().ToConstant(mapper).InSingletonScope();
        }
    }
}