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
            try
            {
                val.CheckIfUserEverCreated();
                var InitialCreation = new CreateInitialUser("Admin", "pass123");
                InitialCreation.CreateRoles();
                InitialCreation.CreateFirstUser();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
