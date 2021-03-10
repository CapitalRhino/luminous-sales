using Models.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class Product : IBaseProperties
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double AmountInStock { get; set; }
        public virtual ICollection<Deal> Deals { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}