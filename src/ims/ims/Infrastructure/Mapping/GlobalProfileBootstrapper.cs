using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace ims.Infrastructure.Mapping
{
    public class GlobalProfileBootstrapper : IProfileBootstrapper
    {
        public IList<Profile> GetProfiles()
        {
            var profiles = GetType().Assembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t)).Select(t => (Profile)Activator.CreateInstance(t)).ToList();
            return profiles;
        }
    }
}