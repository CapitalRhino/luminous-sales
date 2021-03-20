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
                Console.WriteLine("Luminous Sales v0.1");
                view.ActionHandle();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
