using Business.Business.UserManagment;
using Business.Business.UserManagment.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Display
{
    public class InitialSetup
    {
        public InitialSetup()
        {

        }
        public static void InitialUserInput(out string userName, out string password)
        {
            try
            {
                Console.Write("Enter username: ");
                userName = Console.ReadLine();
                Console.Write("Enter password: ");
                password = Console.ReadLine();
            }
            catch (Exception e)
            {
                userName = string.Empty;
                password = string.Empty;
                Console.WriteLine(e.Message);
            }
        }
        public static void InitialRegistration(UserController uc)
        {
            try
            {
                if (uc.CheckIfUserEverCreated())
                {

                }
                else
                {
                    RoleController rc = new RoleController();
                    rc.CreateInitialRoles();
                    string userName, password;
                    InitialUserInput(out userName, out password);
                    uc.RegisterItem(userName, password);
                    Console.WriteLine("Registration succesful!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
