using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models.Interfaces
{
    interface IBaseSalesProperties
    {
        User User { get; set; }
        ICollection<Product> ProductsADeal { get; set;}
        double Amount { get; set; }
        public byte[] Time { get; set; }
    }
}
