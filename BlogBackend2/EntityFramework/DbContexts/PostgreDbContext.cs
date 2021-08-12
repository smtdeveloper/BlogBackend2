using BlogBackend2.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogBackend2.EntityFramework.DbContexts
{
    public class PostgreDbContext : DbContext
    {
        private string connectionString = "User ID=postgres;Password=EslemBetul;Host=localhost;Port=5432;Database=BlogDb;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
