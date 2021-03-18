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
        private string Username;
        private string Password;
        private UserController userctl;
        public CreateInitialUser(string Username, string Password)
        {
            userctl = new UserController();
            this.Username = Username;
            this.Password = Password;
        }
        public void CreateRoles()
        {
            using (context = new LuminousContext())
            {
                var Admin = new Role("Admin");
                var Manager = new Role("Manager");
                var Cashier = new Role("Cashier");
                context.Role.AddRange(Admin, Manager, Cashier);
                context.SaveChanges();
            }
        }
        public void CreateFirstUser()
        {
            using (context = new LuminousContext())
            {
                int roleToAttach = context.Role.FirstOrDefault(r => r.Name == "Admin").Id;
                userctl.RegisterItem(this.Username, this.Password, roleToAttach);
            }
        }

    }
}
