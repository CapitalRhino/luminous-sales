using Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class Role : BaseUserManagmentEntity
    {
        public Role() : base(){}
        public Role(string Name, ICollection<Permission> Permissions)  : base(Name)
        {
            this.Permissions = Permissions;
        }
        [Required]
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}