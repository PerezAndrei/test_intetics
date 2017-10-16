using AutoMapper;

namespace ims.Mapping.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<DataAccess.Entities.User, Models.UserVM>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<Models.UserVM, DataAccess.Entities.User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}