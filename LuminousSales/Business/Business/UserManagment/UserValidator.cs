using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Business.UserManagment
{
    public class UserValidator
    {
        private LuminousContext context;
        public bool CheckIfUserIsCreated()
        {
            using (context = new LuminousContext())
            {
                if (context.User.ToList().Any())
                {
                    return true;
                }
                return false;
            }
        }
        public bool CheckPassword(string Password)
        {
            using (context = new LuminousContext())
            {
                if (context.User.ToList().Exists(user => user.Password == Password))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
