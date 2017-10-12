using AutoMapper;

namespace ims.Mapping.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<DataAccess.Entities.User, Models.UserVM>()
                .PreserveReferences();
            CreateMap<Models.UserVM, DataAccess.Entities.User>()
                .PreserveReferences();
        }
    }
}