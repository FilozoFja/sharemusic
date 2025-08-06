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
                .ForMember(dest => dest.AccessToken, opt => opt.MapFrom(src => src.AccessToken ?? string.Empty)) // Jeśli src.AccessToken jest null, ustawiamy string.Empty
                .ForMember(dest => dest.ExpiresIn, opt => opt.MapFrom(src => src.ExpiresIn == null ? 0 : src.ExpiresIn)) // Jeśli ExpiresIn jest null, ustawiamy 0
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State ?? string.Empty)) // Jeśli src.State jest null, ustawiamy string.Empty
                .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src.Scope ?? string.Empty)) // Jeśli src.Scope jest null, ustawiamy string.Empty
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken ?? string.Empty)) // Jeśli src.RefreshToken jest null, ustawiamy string.Empty
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresIn == null ? DateTime.UtcNow : DateTime.UtcNow.AddSeconds(src.ExpiresIn))); // Jeśli ExpiresIn jest null, ustawiamy ExpiresAt na obecny czas

            CreateMap<SpotifyTokenRequestModel, SpotifyTokenRequestModelDTO>();

            CreateMap<SpotifyTokenRequestModel, SpotifyTokenRequestModelDTO>().ReverseMap();
        }
    }
}
