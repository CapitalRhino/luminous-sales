using Models.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class Stock : IBaseSalesProperties
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public double Amount { get; set; }
        [Timestamp]
        public byte[] time;
    }
}
