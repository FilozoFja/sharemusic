using AutoMapper;
using sharemusic.DTO.Album;

namespace sharemusic.Mapper
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<sharemusic.Models.AlbumModel, AlbumShortModelDTO>()
                .ForMember(dest => dest.SpotifyId, opt => opt.MapFrom(src => src.SpotifyId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.CoverImageUrl))
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist)) 
                .ReverseMap();
        }
    }
}