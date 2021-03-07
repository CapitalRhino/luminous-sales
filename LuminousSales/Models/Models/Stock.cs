using Models.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    class Stock : IBaseSalesProperties
    {
        public User User { get; set; }
        public ICollection<Product> ProductsADeal { get; set; }
        public double Amount { get; set; }
        [Timestamp]
        public byte[] Time { get; set; }
    }
}
