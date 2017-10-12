using System.Collections.Generic;
using AutoMapper;

namespace ims.Infrastructure.Mapping
{
    public interface IProfileBootstrapper
    {
        IList<Profile> GetProfiles();
    }
}
