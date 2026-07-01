using GameVaultJs.Models;
using GameVaultJs.Helpers;
using Microsoft.EntityFrameworkCore;

namespace GameVaultJs.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Game>Games { get; set; }
        public DbSet<Genre>Genres { get; set; }
        public DbSet<News>News { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@gamevault.com",
                    Password = HashHelper.HashPassword("adminpassword"),
                    Role = "Admin"
                },
                new User
                {
                    Id = 2,
                    Email = "user@gamevault.com",
                    Password = HashHelper.HashPassword("userpassword"),
                    Role = "User"
                }
            );
        }
    }
}
