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
        public BaseView(User currentUser)
        {
            this.currentUser = currentUser;
            this.dealctrl = new DealController(currentUser);
        }
        public virtual void ShowAvaliableCommands()
        {
            Console.WriteLine();
            Console.WriteLine("=== MAIN MENU ===");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Sales");
        }
        public virtual void ActionHandle()
        {
            try
            {
                while (true)
                {
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
        public void SaleHandle()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("Deal Managment");
                Console.WriteLine("1. Search");
                Console.WriteLine("2. Sale");
                Console.WriteLine("3. Back");
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
                        case 3:
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
        private void SearchItem()
        {
            try
            {
                productctrl = new ProductController(currentUser);
                Console.Write("Search item: ");
                string search = Console.ReadLine();
                foreach (var item in productctrl.GetByApproximateName(search).ToList())
                {
                    Console.WriteLine($"{item.Id} {item.Name} {item.Price} {item.AmountInStock}");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        private void SaleItem()
        {
            try
            {
                List<Product> check = new List<Product>();
                bool endTyped = false;
                while (!endTyped)
                {
                    Console.Write("Type in item id or name: ");
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
                        Console.WriteLine("Check");
                        double sum = 0;
                        var lastdeals = dealctrl.GetAll().OrderByDescending(x => x.Id).ToArray();
                        for (int i = 0; i < check.Count; i++)
                        {
                            sum += check[i].Price * lastdeals[i].Amount;
                            int rowNum = i + 1;
                            Console.WriteLine($"{rowNum} {check[i].Name} {check[i].Price}x{lastdeals[i].Amount}");
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
