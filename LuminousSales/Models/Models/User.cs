using Models.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class User : IBaseProperties
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Passcode { get; set; }
        public virtual Role UsersRoles { get; set; }
    }
}
