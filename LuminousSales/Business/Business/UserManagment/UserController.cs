using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Business.UserManagment
{
    public class UserController
    {
        private LuminousContext context;
        public void CreateRole(string RoleName, ICollection<Permission> Permissions)
        {
            using (context = new LuminousContext())
            {
                var firstRole = new Role(RoleName, Permissions);
                context.Role.Add(firstRole);
                context.SaveChanges();
            }
        }
        public void CreateUser(string Username, string Password, Role Role)
        {
            using (context = new LuminousContext())
            {
                var firstUser = new User(Username, Password, Role);
                context.User.Add(firstUser);
                context.SaveChanges();
            }
        }
    }
}
