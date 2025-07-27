﻿using JWT_Auth.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWT_Auth.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
    }

}
