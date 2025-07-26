using JWT_Auth.Enitities;
using Microsoft.EntityFrameworkCore;

namespace JWT_Auth.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
    
}
