using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sharemusic.DTO;
using sharemusic.Interface;
using sharemusic.Models;

namespace sharemusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public TokenController(IMapper mapper,ITokenService tokenService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(_tokenService));
        }


        [HttpPost]
        public async Task<IActionResult> SaveToken(SpotifyTokenRequestModelDTO spotifyTokenRequestModelDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            try
            {
                var spotifyToken = _mapper.Map<SpotifyTokenRequestModel>(spotifyTokenRequestModelDTO);
                await _tokenService.SaveToken(spotifyToken);
                return Ok(new { message = "Token saved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while saving the token", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTokenStatus()
        {
            try
            {
                var tokenStatus = await _tokenService.IsTokenExpiredAsync();
                var tokenValid = await _tokenService.IsTokenValidAsync();
                return Ok(new
                {
                    IsTokenExpired = tokenStatus,
                    IsTokenValid = tokenValid
                });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while checking the token status", error = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> ClearToken()
        {
            try
            {
                await _tokenService.ClearTokenAsync();
                return Ok(new { message = "Token cleared successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while clearing the token", error = ex.Message });
            }
        }
    }
}
