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
        ProductController productctrl;
        DealController dealctrl;
        public BaseView(User currentUser)
        {
            this.productctrl = new ProductController(currentUser);
            this.dealctrl = new DealController(currentUser);
        }
        public virtual void ShowAvaliableCommands()
        {
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Sales");
        }
        public virtual void ActionHandle()
        {
            ShowAvaliableCommands();
            Console.Write("> ");
            try
            {
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
                Console.WriteLine("1. Search");
                Console.WriteLine("2. Sale");
                Console.WriteLine("3. Exit");
                Console.Write("Your choice: ");
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
                Console.Write("Search item: ");
            string search = Console.ReadLine();
            ICollection<Product> productsFound = productctrl.GetByApproximateName(search).ToArray();
            foreach (var item in productsFound)
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
                Console.Write("Type in item id or name: ");
                string itemInput = Console.ReadLine();
                int itemId;
                if (Int32.TryParse(itemInput, out itemId))
                {
                    var productToAdd = productctrl.Get(itemId);
                    Console.Write("Amount: ");
                    double amount = double.Parse(Console.ReadLine());
                    dealctrl.Add(itemId, amount);
                }
                else
                {
                    var productToAdd = productctrl.Get(itemInput);
                    Console.Write("Amount: ");
                    double amount = double.Parse(Console.ReadLine());
                    dealctrl.Add(itemInput, amount);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
    }
}
