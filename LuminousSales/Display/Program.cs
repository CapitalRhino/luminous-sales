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
        static void Main(string[] args)
        {
            try
            {
                BigLogo();
                Console.WriteLine("Luminous Sales v0.1 by A. Konarcheva, D. Byalkov & D. Todorov 2021");
                Console.WriteLine();
                var uc = new UserController();
                InitialSetup.InitialRegistration(uc);
                Console.Write("Enter password: ");
                User currentUser = uc.ValidatePassword(Console.ReadLine());
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
        private static void BigLogo()
        {
            Console.WriteLine("  _                     _                          _____       _          ");
            Console.WriteLine(" | |                   (_)                        / ____|     | |         ");
            Console.WriteLine(" | |    _   _ _ __ ___  _ _ __   ___  _   _ ___  | (___   __ _| | ___ ___ ");
            Console.WriteLine(" | |   | | | | '_ ` _ \\| | '_ \\ / _ \\| | | / __|  \\___ \\ / _` | |/ _ / __|");
            Console.WriteLine(" | |___| |_| | | | | | | | | | | (_) | |_| \\__ \\  ____) | (_| | |  __\\__ \\");
            Console.WriteLine(" |______\\__,_|_| |_| |_|_|_| |_|\\___/ \\__,_|___/ |_____/ \\__,_|_|\\___|___/");
        }
    }
}
