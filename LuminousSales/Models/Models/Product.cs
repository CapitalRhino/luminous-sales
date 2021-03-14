using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class Product
    {
        public Product(){}
        public Product(string Name, double Price)
        {
            this.Name = Name;
            this.Price = Price;
            this.AmountInStock = 0;
        }
        public Product(string Name, double Price, double AmountInStock)
        {
            this.Name = Name;
            this.Price = Price;
            this.AmountInStock = AmountInStock;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double AmountInStock { get; set; }
    }
}