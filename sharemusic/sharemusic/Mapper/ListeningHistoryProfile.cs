    using AutoMapper;
    using sharemusic.DTO.ListeningHistory;
    using sharemusic.Models;
    using sharemusic.DTO.GenreModel;

    namespace sharemusic.Mapper
    {
        public class ListeningHistoryProfile : Profile
        {
            public ListeningHistoryProfile()
            {
                CreateMap<ListeningHistoryModel, ListeningHistoryModelDTO>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.SongShort, opt => opt.MapFrom(src => src.Song))
                    .ForMember(dest => dest.PlaylistShort, opt => opt.MapFrom(src => src.Playlist))
                    .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.DateTime))
                    .ForMember(dest => dest.GenreShortModelDTO, opt => opt.MapFrom(src => src.Genre));
            }
        }
    }