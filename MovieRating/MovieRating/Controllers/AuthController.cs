using Microsoft.AspNetCore.Mvc;
using MovieRating.Helpers;

namespace MovieRating.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly Dictionary<string, string> _refreshTokens = new();

        [HttpGet("generate")]
        public IActionResult GenerateTokenPair()
        {
            var userId = Guid.NewGuid().ToString();

            var accessToken = JwtHelper.GenerateJwtToken(userId);
            var refreshToken = JwtHelper.GenerateRefreshToken();

            _refreshTokens[refreshToken] = userId;

            return Ok(new
            {
                accessToken,
                refreshToken
            });
        }

        [HttpPost("refresh")]
        public IActionResult RefreshToken([FromBody] string oldRefreshToken)
        {
            if (!_refreshTokens.ContainsKey(oldRefreshToken))
                return Unauthorized("Invalid refresh token");

            var userId = _refreshTokens[oldRefreshToken];

            var newAccessToken = JwtHelper.GenerateJwtToken(userId);
            var newRefreshToken = JwtHelper.GenerateRefreshToken();

            _refreshTokens.Remove(oldRefreshToken);
            _refreshTokens[newRefreshToken] = userId;

            return Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        }
    }
}
