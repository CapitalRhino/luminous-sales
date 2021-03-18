using Data.Base;
using System;
using System.Collections.Generic;

using System.Text;

namespace Models.Models
{
    public class Stock : BaseSales
    {
        public Stock() : base(){}
        public Stock(int UserId, int ProductId, double Amount) : base(UserId, ProductId, Amount){}
    }
}
