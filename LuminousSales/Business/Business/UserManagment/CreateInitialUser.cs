using System;
using Models;
using System.Linq;
using Models.Models;
using System.Collections.Generic;

namespace Business.Business.UserManagment
{
    public class CreateInitialUser
    {
        private LuminousContext context;
        public void CreatePermissions()
        {
            using (context = new LuminousContext())
            {
                var admin = new Permission("Admin");
                var roleChanger = new Permission("Role Changer");
                var userCreation = new Permission("User Creation");
                var report = new Permission("Report");
                var stock = new Permission("Stock");
                var sell = new Permission("Sell");
                context.Permission.AddRange
                    (
                        admin,
                        roleChanger,
                        userCreation,
                        report,
                        stock,
                        sell
                    );
                context.SaveChanges();
                Console.WriteLine("Permissions were intialized");
            }
        }

        public void CreateFirstRole(string Name, ICollection<Permission> Permissions)
        {
            using (context = new LuminousContext())
            {
                var firstRole = new Role(Name, Permissions);
                context.Role.Add(firstRole);
                context.SaveChanges();
            }
        }
        public void CreateFirstUser(string Name, string Password, Role Role)
        {
            using (context = new LuminousContext())
            {
                var firstUser = new User(Name, Password, Role);
                context.User.Add(firstUser);
                context.SaveChanges();
            }
        }

    }
}
