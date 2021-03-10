using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace LuminousSales.Data
{
    public class UsersToRolesContext:DbContext
    {
        public UsersToRolesContext():base()
        {

        }
        public DbSet<Role> Role { get; set; }
           

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LuminousSales;Integrated Security = true;");
        }

    }
}
