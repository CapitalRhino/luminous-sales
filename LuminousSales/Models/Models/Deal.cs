using Data.Base;
using System.Collections.Generic;

namespace Models.Models
{
    public class Deal : BaseSales
    {
        public Deal() : base(){}
        public Deal(int UserId, int ProductId, double Amount) : base(UserId, ProductId, Amount) { }
    }
}