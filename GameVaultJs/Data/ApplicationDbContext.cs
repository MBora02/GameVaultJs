using GameVaultJs.Models;
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
    }
}
