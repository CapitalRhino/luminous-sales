using Business.Business.UserManagment;
using Display.Views;
using Models;
using Models.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Display
{
   public class Program
    {
        static void Main()
        {
            User currentUser = new User();
            var uc = new UserController();
            try
            {
                BigLogo();
                InitialSetup.InitialRegistration(uc); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            bool isRunning = true;
            while (isRunning)
            {
                try
                {
                    Console.Write("Enter password: ");
                    currentUser = uc.ValidatePassword(Console.ReadLine());
                    if (currentUser != null)
                    {
                        isRunning = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            try
            {
                uc = new UserController(currentUser);
                var view = new BaseView(currentUser);
                switch (currentUser.RoleId)
                {
                    case 1:
                        view = new CashierView(currentUser);
                        break;
                    case 2:
                        view = new ManagerView(currentUser);
                        break;
                    case 3:
                        view = new AdminView(currentUser);
                        break;
                    default:
                        break;
                }
                view.ActionHandle();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Logo splash screen
        /// </summary>
        private static void BigLogo()
        {
            Console.WriteLine("  _                     _                          _____       _          ");
            Console.WriteLine(" | |                   (_)                        / ____|     | |         ");
            Console.WriteLine(" | |    _   _ _ __ ___  _ _ __   ___  _   _ ___  | (___   __ _| | ___ ___ ");
            Console.WriteLine(" | |   | | | | '_ ` _ \\| | '_ \\ / _ \\| | | / __|  \\___ \\ / _` | |/ _ / __|");
            Console.WriteLine(" | |___| |_| | | | | | | | | | | (_) | |_| \\__ \\  ____) | (_| | |  __\\__ \\");
            Console.WriteLine(" |______\\__,_|_| |_| |_|_|_| |_|\\___/ \\__,_|___/ |_____/ \\__,_|_|\\___|___/");
            Console.WriteLine("Luminous Sales v0.1.1 by A. Konarcheva, D. Byalkov & D. Todorov 2021");
            Console.WriteLine();
        }
    }
}
