using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace LuminousSales.Data
{
    public class RoleContext : DbContext
    {
        public RoleContext():base()
        {
           
        }


        public DbSet<Role> Role { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server= (localdb)\mssqllocaldb;Database=LuminousSales;Integrated Security = true;");
        }
    }
      

       

}

