using Data;
using Data.Models;
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

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Deal> Deal { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<RolePermission> RolePermission { get; set; }

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
            modelBuilder.Entity<Permission>()
                .HasIndex(permission => new { permission.Name })
                .IsUnique(true);
            modelBuilder.Entity<Product>()
                .HasIndex(product => new { product.Name })
                .IsUnique(true);
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermisionId });
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Roles)
                .WithMany(rp => rp.Permissions)
                .HasForeignKey(rp => rp.RoleId);
            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(rp => rp.Role)
                .HasForeignKey(rp => rp.PermisionId);
        }
    }
}
