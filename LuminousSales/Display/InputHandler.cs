using System;
using System.Collections.Generic;
using System.Text;

namespace Display
{
    class InputHandler
    {
        public void CommandLineInterface()
        {
            ShowAvaliableCommands();
            while (true)
            {
                Console.WriteLine("Select action");
                Console.WriteLine("Sales, UserManagment");
                string input = "";
                try
                {
                    Console.Write("> ");
                    input = Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                switch (input)
                {
                    case "sales": break;
                    case "usermanagment": break;
                    default: break;
                }
            }
        }
        public void ShowAvaliableCommands()
        {
            Console.WriteLine("asad");
            Console.WriteLine("asad");
            Console.WriteLine("asad");
            Console.WriteLine("asad");
            Console.WriteLine("asad");
        }
        public void Sales()
        {
            try
            {
                Console.Write("> ");
                string input = Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
