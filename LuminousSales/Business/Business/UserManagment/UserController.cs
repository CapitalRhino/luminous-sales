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
        public void CreateUser(string Username, string Password, int RoleId)
        {
            using (context = new LuminousContext())
            {
                var user = new User(Username, Password, RoleId);
                context.User.Add(user);
                context.SaveChanges();
            }
        }
    }
}
