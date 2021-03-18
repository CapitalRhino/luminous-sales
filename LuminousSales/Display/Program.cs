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
            try
            {
                var a = new UserController();
                a.UpdateRole(1, "Cashier");
                foreach (var item in a.GetAll())
                {
                    Console.WriteLine($"{item.Name} {item.Role.Name}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
