using Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Models
{
    public class User : BaseUserManagmentEntity
    {
        public User() : base() { }
        public User(string Name, string Password, int RoleId) : base(Name)
        {
            this.Password = Password;
            this.RoleId = RoleId;
        }
        [Required]
        public string Password { get; set; }
        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
