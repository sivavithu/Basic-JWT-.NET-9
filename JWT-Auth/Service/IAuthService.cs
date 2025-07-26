using JWT_Auth.Enitities;
using JWT_Auth.Models;

namespace JWT_Auth.Service
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(UserDto request);
        public Task<User?> RegisterAsync(UserDto request);
    }
}
