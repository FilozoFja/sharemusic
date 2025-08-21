using sharemusic.DTO.GenreModel;

namespace sharemusic.Mapper
{
    public class GenreProfile : AutoMapper.Profile
    {
        public GenreProfile()
        {
            CreateMap<Models.GenreModel, GenreShortModelDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
