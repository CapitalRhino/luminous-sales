using Models.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class Deal : IBaseSalesProperties
    {
        public User User { get; set; }
        public ICollection<Product> ProductsADeal { get; set; }
        public double Amount { get; set; }
        [Timestamp]
        public byte[] Time { get; set; }
    }
}