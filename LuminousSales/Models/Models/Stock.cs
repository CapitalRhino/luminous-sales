using Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class Stock : BaseSales
    {
        public Stock() : base(){}
        public Stock(User User, ICollection<Product> Products) : base(User, Products){}
    }
}
