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
        public void CheckIfUserEverCreated()
        {
            using (context = new LuminousContext())
            {
                if (context.User.ToList().Any())
                {
                    throw new ArgumentException("First user is already created!");
                }
            }
        }
        public void CheckPassword(string Password)
        {
            using (context = new LuminousContext())
            {
                if (context.User.ToList().Exists(user => user.Password == Password))
                {
                    throw new ArgumentException("Invalid User!");
                }
            }
        }
    }
}
