using Business.Business.UserManagment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Display.InitialSetup
{
    public class InitialSetup
    {
        public InitialSetup()
        {

        }
        public string[] InitialUserInput()
        {
            string userName = "", password = "";
            try
            {
                Console.Write("Enter username: ");
                userName = Console.ReadLine();
                Console.Write("Enter password: ");
                password = Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new string[] { userName, password };
        }
        public void InitialRegistration()
        {
            var uc = new UserController();
            try
            {
                if (uc.CheckIfUserEverCreated())
                {

                }
                else
                {
                    uc.RegisterItem(InitialUserInput()[0], InitialUserInput()[1]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
