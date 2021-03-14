using Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class User : BaseUserManagmentEntity
    {
        public User() : base() { }
        public User(string Name, string Password, Role Role) : base(Name)
        {
            this.Password = Password;
            this.Role = Role;
        }
        [Required]
        public string Password { get; set; }
        [Required]
        public virtual Role Role { get; set; }
    }
}
