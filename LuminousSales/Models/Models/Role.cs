using Data.Base;
using Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class Role : BaseUserManagmentEntity
    {
        public Role() : base(){}
        public Role(string Name)  : base(Name)
        {
        }
        public virtual ICollection<RolePermission> Permissions { get; set; } = new List<>
    }
}