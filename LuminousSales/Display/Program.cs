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
            var a = new InitialSetup.InitialSetup();
            a.InitialRegistration();
            var ih = new InputHandler();
            ih.CommandLineInterface();
        }
    }
}
