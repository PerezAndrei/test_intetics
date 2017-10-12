using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ims.Infrastructure.Mapping;

namespace ims.Configuration.Ninject
{
    public class WebAppProfileBootstrapper : IProfileBootstrapper
    {
        public IList<Profile> GetProfiles()
        {
            return GetType().Assembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t))
                .Select(t => (Profile)Activator.CreateInstance(t)).ToList();
        }
    }
}