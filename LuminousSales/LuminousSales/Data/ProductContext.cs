using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using LuminousSales.Data.Model;

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
