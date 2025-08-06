using AutoMapper;
using sharemusic.DTO;
using sharemusic.Models;

namespace sharemusic.Mapper
{
    public class SongProfile : Profile
    {
        public SongProfile()
        {
            CreateMap<SongModelDTO, SongModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
                .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.IsDraft, opt => opt.MapFrom(src => src.IsDraft))
                .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.CoverImageUrl));
        }
    }
}
