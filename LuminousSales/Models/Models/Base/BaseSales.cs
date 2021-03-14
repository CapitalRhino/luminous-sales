using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Base
{
    public abstract class BaseSales
    {
        protected BaseSales()
        {

        }
        protected BaseSales(User User, ICollection<Product> Products)
        {
            this.User = User;
            this.Products = Products;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        public virtual ICollection<Product> Products { get; set; }
        [Timestamp]
        [Required]
        public byte[] Time { get; set; }
    }
}
