using System;
using System.Collections.Generic;
using System.Text;

namespace Display
{
    class InputHandler
    {
        public void CommandLineInterface()
        {
            while (true)
            {
                Console.WriteLine("Select action");
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
