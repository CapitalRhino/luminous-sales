using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using LuminousSales.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace LuminousSales.Data
{
   public class ProductContext : DbContext
    {
        public ProductContext():base("name = ProductContext")
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
