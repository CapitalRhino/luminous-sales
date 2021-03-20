using Data;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Models
{
    public class LuminousContext : DbContext
    {
        public LuminousContext()
        {

        }

        public LuminousContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Deal> Deal { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(Configuration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(user => new { user.Name, user.Password })
                .IsUnique(true);
            modelBuilder.Entity<Role>()
                .HasIndex(role => new { role.Name })
                .IsUnique(true);
            modelBuilder.Entity<Product>()
                .HasIndex(product => new { product.Name })
                .IsUnique(true);
        }
    }
}
