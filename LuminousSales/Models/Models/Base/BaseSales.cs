using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Base
{
    public abstract class BaseSales
    {
        protected BaseSales()
        {

        }
        protected BaseSales(int UserId, int ProductId, double Amount)
        {
            this.UserId = UserId;
            this.ProductId = ProductId;
            this.Amount = Amount;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public virtual User User { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required]
        public virtual Product Products { get; set; }
        [Required]
        public double Amount { get; set; }
        // [DataType(DataType.DateTime)]
        [Required]
        public DateTime Time { get; set; }
    }
}
