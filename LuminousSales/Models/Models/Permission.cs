using Models.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class Permission : IBaseProperties
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Role> Role { get; set; }
    }
}