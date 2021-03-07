using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace LuminousSales.Data
{
    public class RoleContext : DbContext
    {
        public RoleContext():base("name = RoleContext")
        {

        }
        public DbSet<Role> Roles { get; set; }
    }
}
