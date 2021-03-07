using Microsoft.EntityFrameworkCore;


namespace LuminousSales.Data
{
    public class RoleContext : DbContext
    {
        public RoleContext():base()
        {

        }
        public DbSet<Role> Roles { get; set; }

       
    }
}
