using AutoMapper;
using sharemusic.DTO;
using sharemusic.Models;

namespace sharemusic.Mapper
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<SpotifyTokenRequestModelDTO, SpotifyTokenRequestModel>()
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken))
                .ForMember(dest => dest.ExpiresIn, opt => opt.MapFrom(src => src.ExpiresIn))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src.Scope))
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken))
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => DateTime.UtcNow.AddSeconds(src.ExpiresIn)));

            CreateMap<SpotifyTokenRequestModel, SpotifyTokenRequestModelDTO>();

            CreateMap<SpotifyTokenRequestModel, SpotifyTokenRequestModelDTO>().ReverseMap();
        }
    }
}
