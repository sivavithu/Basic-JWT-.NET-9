using JWT_Auth.Data;
using JWT_Auth.Enitities;
using JWT_Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JWT_Auth.Service
{
    public class AuthService(ApplicationDbContext context, IConfiguration configuration) : IAuthService
    {

      public  async Task<string?> LoginAsync(UserDto request)
        {
            var user= await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user is null)
            {
                return null; // User not found
            }
            if (user.Username != request.Username)
            {
                return BadRequest("User not Found");
            }

            var result = new PasswordHasher<User>()
                            .VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return BadRequest("Wrong Password");
            }

            string token = CreateToken(user);

            return Ok(token); ;
        }

        public async Task<User?> RegisterAsync(UserDto request)
        {
            if(await context.Users.AnyAsync(i => i.Username == request.Username))
            {
                return null; // User already exists
            }
            var user = new User();
           
            var hashedPassword = new PasswordHasher<User>()
                                   .HashPassword(user, request.Password);
            user.Username = request.Username;
            user.PasswordHash = hashedPassword;
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
