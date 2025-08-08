using AutoMapper;
using sharemusic.DTO;
using sharemusic.DTO.SongModel;
using sharemusic.Models;
using SpotifyAPI.Web;

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
                .ForMember(dest => dest.IsDraft, opt => opt.MapFrom(src => src.IsDraft))
                .ForMember(dest => dest.ArtistId, opt => opt.MapFrom(src => src.ArtistId))
                .ForMember(dest => dest.LocalSongPath, opt => opt.MapFrom(src => src.LocalSongPath))
                .ForMember(dest => dest.SongLengthInSeconds, opt => opt.MapFrom(src => src.SongLengthInSeconds))
                .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.CoverImageUrl));
            CreateMap<SongModel, SongModelDTO>();
            CreateMap<SongShortModelDTO, SongModel>();
            _ = CreateMap<FullTrack, SongModel>()
                .ForMember(dest => dest.SpotifyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => string.Join(", ", src.Artists.Select(a => a.Name))))
                .ForMember(dest => dest.Album, opt => opt.MapFrom(src => src.Album.Name))
                .ForMember(dest => dest.CoverImageUrl, opt => opt.MapFrom(src => src.Album.Images.FirstOrDefault().Url));
        }
    }
}
