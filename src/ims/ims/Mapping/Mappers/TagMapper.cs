using AutoMapper;

namespace ims.Mapping.Mappers
{
    public class TagMapper : Profile
    {
        public TagMapper()
        {
            CreateMap<DataAccess.Entities.Tag, Models.TagVM>()
                .PreserveReferences();
            CreateMap<Models.TagVM, DataAccess.Entities.Tag>()
                .PreserveReferences();
        }
    }
}