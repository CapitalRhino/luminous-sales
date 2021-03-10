using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace LuminousSales.Data
{
    public class RolesToPermissionContext:DbContext
    {
        public RolesToPermissionContext():base()
        {
           

        }


        public DbSet<Role> RoleToPermission { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server= (localdb)\mssqllocaldb;Database=LuminousSales;Integrated Security = true;");
        }
    }
       
    }

