using Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class Deal : BaseSales
    {
        public Deal() : base(){}
        public Deal(User User, ICollection<Product> Products) : base(User, Products){}
    }
}