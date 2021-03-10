using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace LuminousSales.Data
{
   public class UserContext:DbContext
    {
        public UserContext():base()
        {

        }
       
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server= (localdb)\mssqllocaldb;Database=LuminousSales;Integrated Security = true;");
        }


    }
}
