using Models.Models.Interfaces;
using System.Collections.Generic;


namespace Models.Models
{
    public class Role : IBaseProperties
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> UsersWithTheRole { get; set; }
        public ICollection<Permission> RolesPermissions{ get; set; }
    }
}