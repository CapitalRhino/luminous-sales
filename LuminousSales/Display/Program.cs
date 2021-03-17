using Business.Business.UserManagment;
using Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Display
{
   public class Program
    {
        static void Main(string[] args)
        {
            var val = new UserValidator();
            if (!val.CheckIfUserIsCreated())
            {
                var InitialCreation = new CreateInitialUser("Admin", "Admin", "pass123");
                InitialCreation.CreateFirstRole();
                InitialCreation.CreateFirstUser();
            }
            else
            {
                Console.WriteLine("Already created");
            }
        }
    }
}
