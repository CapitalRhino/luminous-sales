using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace LuminousSales.Data
{
    public class RolesToPermissionContext:DbContext
    {
        public RolesToPermissionContext():base("name = RolesToPermissionContext")
        {

        }
        public DbSet<Role> Roles { get; set; }
    }
}
