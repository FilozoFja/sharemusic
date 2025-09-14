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
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State ?? string.Empty)) // Jeśli src.State jest null, ustawiamy string.Empty
                .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src.Scope ?? string.Empty)) // Jeśli src.Scope jest null, ustawiamy string.Empty
                .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src => src.RefreshToken ?? string.Empty)) // Jeśli src.RefreshToken jest null, ustawiamy string.Empty
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => DateTime.UtcNow)); // Ustawiamy AddedAt na obecny czas

            CreateMap<SpotifyTokenRequestModel, SpotifyTokenRequestModelDTO>().ReverseMap();
        }
    }
}
