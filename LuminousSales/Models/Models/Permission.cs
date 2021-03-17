using Data.Base;
using Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class Permission : BaseUserManagmentEntity
    {
        public Permission() : base(){}
        public Permission(string Name) : base(Name){}
        [Required]
        public virtual ICollection<RolePermission> Role { get; set; }
    }
}