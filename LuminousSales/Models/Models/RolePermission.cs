using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public virtual Role Roles { get; set; }
        public int PermisionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
