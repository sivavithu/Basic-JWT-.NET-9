using JWT_Auth.Entities;
using JWT_Auth.Models;
using JWT_Auth.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWT_Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
          
            var user = await authService.RegisterAsync(request);
            if(user is null)
            {
                return BadRequest("User already exists");
            }
            return Ok(user);


        }
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var results = await authService.LoginAsync(request);
            if (results is null)
            {
                return BadRequest("Invalid username or Password");
            }
            return Ok(results);
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var results = await authService.RefreshTokenAsync(request);
            if (results is null || results.AccessToken is null || results.RefreshToken is null)
            {
                return Unauthorized("Invalid Refresh Token");
            }
            return Ok(results);
        }
        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are authenticated");
        }



    }
}
