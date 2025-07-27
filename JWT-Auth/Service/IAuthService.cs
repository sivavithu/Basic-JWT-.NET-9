using JWT_Auth.Entities;
using JWT_Auth.Models;

namespace JWT_Auth.Service
{
    public interface IAuthService
    {
        public Task<TokenResponseDto?> LoginAsync(UserDto request);
        public Task<User?> RegisterAsync(UserDto request);

        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
