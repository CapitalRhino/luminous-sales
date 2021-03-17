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
        private string RoleName;
        private string Username;
        private string Password;
        private UserController userctl;
        public CreateInitialUser(string RoleName, string Username, string Password)
        {
            userctl = new UserController();
            this.RoleName = RoleName;
            this.Username = Username;
            this.Password = Password;
        }
        public void CreatePermissions()
        {
            
            using (context = new LuminousContext())
            {
                var admin = new Permission("Admin");
                var roleChanger = new Permission("Role Creator");
                var userCreation = new Permission("User Creator");
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

        public void CreateFirstRole()
        {
            using (context = new LuminousContext())
            {
                var AdminPermission = context.Permission.FirstOrDefault(p => p.Name == "Admin");
                userctl.CreateRole(this.RoleName , AdminPermission);
            }
        }
        public void CreateFirstUser()
        {
            using (context = new LuminousContext())
            {
                var roleToAttach = context.Role.Where(r => r.Name == this.RoleName).FirstOrDefault();
                userctl.CreateUser(this.Username, this.Password, roleToAttach);
            }
        }

    }
}
