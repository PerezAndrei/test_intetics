using AutoMapper;

namespace ims.Mapping.Mappers
{
    public class ImageMapper : Profile
    {
        public ImageMapper()
        {
            CreateMap<DataAccess.Entities.Image, Models.ImageVM>()
                .PreserveReferences();
            CreateMap<Models.ImageVM, DataAccess.Entities.Image>()
                .PreserveReferences();
        }
    }
}