namespace sharemusic.Mapper;
public class ArtistProfile : AutoMapper.Profile
{
    public ArtistProfile()
    {
        CreateMap<Models.ArtistModel, DTO.ArtistModel.ArtistShortModelDTO>()
            .ForMember(dest => dest.SpotifyId, opt => opt.MapFrom(src => src.SpotifyId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl)).ReverseMap();
        CreateMap<Models.ArtistModel, DTO.ArtistModel.ArtistShortModelDTO>().ReverseMap();
    }
}