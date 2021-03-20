using Data.Base;
using System;
using System.Collections.Generic;

namespace Models.Models
{
    public class Deal : BaseSales
    {
        public Deal() : base(){}
        public Deal(int UserId, int ProductId, double Amount, DateTime Time) : base(UserId, ProductId, Amount, Time) { }
    }
}