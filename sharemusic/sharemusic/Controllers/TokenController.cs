using Microsoft.AspNetCore.Mvc;
using sharemusic.DTO;
using sharemusic.Interface;

namespace sharemusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(_tokenService));
        }


        [HttpPost]
        public async Task<IActionResult> SaveToken(SpotifyTokenRequestModelDTO spotifyTokenRequestModelDTO)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            try
            {
                await _tokenService.SaveTokenAsync(spotifyTokenRequestModelDTO);
                return Ok(new { message = "Token saved successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while saving the token", error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetToken()
        {
            try
            {
                var token = await _tokenService.GetAccessTokenAsync();
                return Ok(token);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while getting the token", error = ex.Message });
            }
        }

        [HttpGet("GetTokensString")]
        public async Task<IActionResult> GetTokenString()
        {
            try
            {
                var token = await _tokenService.GetAccessTokenStringAsync();
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while getting the token string", error = ex.Message });
            }
        }

        [HttpGet("IsTokenvalid")]
        public async Task<IActionResult> IsTokenValid()
        {
            try
            {
                var isValid = await _tokenService.IsTokenValidAsync();
                return Ok(new { isValid });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while checking the token validity", error = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteToken()
        {
            try
            {
                await _tokenService.DeleteTokenAsync();
                return Ok(new { message = "Token cleared successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while clearing the token", error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTokenAsync(SpotifyTokenRequestModelDTO spotifyTokenRequestModelDTO)
        {
            try
            {
                await _tokenService.UpdateTokenAsync(spotifyTokenRequestModelDTO);
                return Ok(new { message = "Token updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the token", error = ex.Message });
            }
        }
    }
}
