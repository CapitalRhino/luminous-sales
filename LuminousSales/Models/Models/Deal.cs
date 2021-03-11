using Models.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class Deal : IBaseSalesProperties
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public double Amount { get; set; }
        [Timestamp]
        public byte[] time;
    }
}