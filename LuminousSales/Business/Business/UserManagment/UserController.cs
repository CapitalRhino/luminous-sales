using Data.Models;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Business.Business.UserManagment
{
    public class UserController
    {
        private LuminousContext context;
        public void CreateRole(string RoleName, Permission Permission)
        {
            using (context = new LuminousContext())
            { 
                var role = new Role(RoleName);
                var relationship = new RolePermission();
                relationship.Roles = role;
                relationship.Permission = Permission;
                role.Permissions.Add(relationship);
                Permission.Role.Add(relationship);
                context.RolePermission.Add(relationship);
                context.Role.Add(role);
                context.SaveChanges();
            }
        }
        public void CreateUser(string Username, string Password, Role Role)
        {
            using (context = new LuminousContext())
            {
                var user = new User(Username, Password, Role);
                context.User.Add(user);
                context.SaveChanges();
            }
        }
    }
}
