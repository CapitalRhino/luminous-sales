using System;
using System.Collections.Generic;
using System.Text;

namespace LuminousSales.Data.Model
{
   public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AmountInStock { get; set; }
    }
}
