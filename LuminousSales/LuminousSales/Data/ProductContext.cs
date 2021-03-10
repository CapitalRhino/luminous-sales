using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace LuminousSales.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext():base()
        {

        }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LuminousSales;Integrated Security = true;");
        }

    }
}
