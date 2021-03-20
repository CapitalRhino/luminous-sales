using Business.Business.Sales;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Display.Views
{
    public class BaseView
    {
        internal ProductController productctrl;
        private DealController dealctrl;
        internal User currentUser;

        /// <summary>
        /// Constructor that accepts a user object.
        /// <summary>
        /// <remarks>
        /// User object is used for stock and deal checking.
        /// Initialises stock and deal controllers.
        /// </remarks>
        public BaseView(User currentUser)
        {
            this.currentUser = currentUser;
            this.dealctrl = new DealController(currentUser);
            this.productctrl = new ProductController(currentUser);
        }

        /// <summary>
        /// Shows all available commands.
        /// </summary>
        /// <remarks>
        /// Includes only the basic functions of the program.
        /// The main menu.
        /// </remarks>
        public virtual void ShowAvaliableCommands()
        {
            Console.WriteLine();
            Console.WriteLine("=== MAIN MENU ===");
            Console.WriteLine("0. Exit");
            Console.WriteLine();
            Console.WriteLine("1. Sales");
        }

        /// <summary>
        /// Asks the user to choose which group of action to use.
        /// </summary>
        /// <remarks>
        /// A choice is given by entering a number from the given list.
        /// It's expanded by its inheritors.
        /// </remarks>
        public virtual void ActionHandle()
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    ShowAvaliableCommands();
                    Console.Write("> ");
                    int input = int.Parse(Console.ReadLine());
                    if (input == 0)
                    {
                        Environment.Exit(0);
                    }
                    else if (input == 1)
                    {
                        SaleHandle();
                    }
                    else Console.WriteLine("Invalid operation");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Selection menu with base actions.
        /// </summary>
        public void SaleHandle()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== SALES ===");
                Console.WriteLine("0. Back");
                Console.WriteLine();
                Console.WriteLine("Deal Managment");
                Console.WriteLine("1. Search");
                Console.WriteLine("2. Sale");
                Console.Write("> ");
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            SearchItem();
                            break;
                        case 2:
                            SaleItem();
                            break;
                        case 0:
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Option!");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
        }

        /// <summary>
        /// Lists all products which match the search term from the database.
        /// </summary>
        private void SearchItem()
        {
            try
            {
                productctrl = new ProductController(currentUser);
                Console.Write("Search item: ");
                string search = Console.ReadLine();
                Console.WriteLine("Product ID - Name - Price - Amount");
                foreach (var item in productctrl.GetByApproximateName(search).ToList())
                {
                    Console.WriteLine($"{item.Id} - {item.Name} - {item.Price} - {item.AmountInStock}");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Sells products. Asks which product and the quantity sold. Prints a summary in the end.
        /// </summary>
        private void SaleItem()
        {
            try
            {
                productctrl = new ProductController(currentUser);
                List<Product> check = new List<Product>();
                bool endTyped = false;
                while (!endTyped)
                {
                    Console.Write("Enter item id or name (\"end\" to finish): ");
                    string itemInput = Console.ReadLine();
                    if (itemInput.ToLower() != "end")
                    {
                        int itemId;
                        if (int.TryParse(itemInput, out itemId))
                        {
                            var productToAdd = productctrl.Get(itemId);
                            Console.Write("Amount: ");
                            double amount = double.Parse(Console.ReadLine());
                            dealctrl.Add(itemId, amount, DateTime.Now);
                            check.Add(productToAdd);
                        }
                        else
                        {
                            var productToAdd = productctrl.Get(itemInput);
                            Console.Write("Amount: ");
                            double amount = double.Parse(Console.ReadLine());
                            dealctrl.Add(itemInput, amount, DateTime.Now);
                            check.Add(productToAdd);
                        }
                    }
                    else
                    {
                        endTyped = true;
                        Console.WriteLine();
                        Console.WriteLine("Check");
                        double sum = 0;
                        check.Reverse();
                        var lastdeals = dealctrl.GetAll().OrderByDescending(x => x.Id).ToArray();
                        for (int i = check.Count - 1; i >= 0; i--)
                        {
                            sum += check[i].Price * lastdeals[i].Amount;
                            int rowNum = i + 1;
                            Console.WriteLine($"{rowNum}. {check[i].Name} - {check[i].Price}x{lastdeals[i].Amount}");
                        }
                        Console.WriteLine($"Total: {sum}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
