namespace sharemusic.Mapper;

using sharemusic.DTO.PlaylistModel;
using sharemusic.Models;
using sharemusic.DTO;

public class PlaylistProfile : AutoMapper.Profile
{
    public PlaylistProfile()
    {
        CreateMap<PlaylistModel, PlaylistModelDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => src.CoverUrl))
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.OwnerName))
            .ReverseMap();

        CreateMap<PlaylistModel, PlaylistShortModelDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.CoverUrl))
            .ForMember(dest => dest.SpotifyId, opt => opt.MapFrom(src => src.SpotifyId))
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.OwnerName))
            .ReverseMap();
    }
}
       