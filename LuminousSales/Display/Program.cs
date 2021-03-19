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
                User currentUser = uc.ValidatePassword("admin123");
                uc = new UserController(currentUser);
                var cv = new CashierView(currentUser);
                cv.SaleHandle();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
