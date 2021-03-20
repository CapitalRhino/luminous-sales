using System;
using System.Collections.Generic;
using System.Text;
using Business.Business.Sales;
using Models.Models;

namespace Display.Views
{
    public class ManagerView : BaseView
    {
        StockController stockctrl;
        DealController dealctrl;

        /// <summary>
        /// Constructor that accepts a user object.
        /// <summary>
        /// <remarks>
        /// User object is used for stock and deal checking.
        /// </remarks>
        /// <remarks>
        /// Initialises stock and deal controllers.
        /// </remarks>

        public ManagerView(User currentUser):base(currentUser)
        {
            stockctrl = new StockController(currentUser);
            dealctrl = new DealController(currentUser);
        }

       /// <summary>
       /// Shows all available commands.
       /// </summary>
       /// <remarks>
       /// Inherits all available commands from the base view.
       /// </remarks>
       /// <remarks>
       /// The main menu.
       /// </remarks>


        public override void ShowAvaliableCommands()
        {
            base.ShowAvaliableCommands();
            Console.WriteLine("2. Stock");
        }

        /// <summary>
        /// Asks the user to choose which group of action to use.
        /// </summary>
        /// <remarks>
        /// If user inputs the digit 1, returns selling handles.
        /// </remarks>
        /// <remarks>
        /// If user inputs the digit 2, returns managing handles.
        /// </remarks>
        /// <remarks>
        /// If user inputs something else, the operation is invalid.
        /// </remarks>

        public override void ActionHandle()
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
                    else if (input == 2)
                    {
                        ManageHandle();
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
        /// Selection menu with manager actions.
        /// </summary>
        /// <remarks>
        /// Requires role level 2 (Manager).
        /// </remarks>

        public void ManageHandle()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== STOCK ===");
                Console.WriteLine("0. Back");
                Console.WriteLine();
                Console.WriteLine("Stock Managment");
                Console.WriteLine("1. GetAll");
                Console.WriteLine("2. Get");
                Console.WriteLine("3. GetByTime");
                Console.WriteLine("4. Add");
                Console.WriteLine("5. Delete");
                Console.Write("> ");
                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            GetAll();
                            break;
                        case 2:
                            Get();
                            break;
                        case 3:
                            GetByTime();
                            break;
                        case 4:
                            Add();
                            break;
                        case 5:
                            Delete();
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
        /// Lists all information about stock from the database.
        /// </summary>
       
        public void GetAll()
        {
            try
            {
               Console.WriteLine("Getting all stock...");
                Console.WriteLine("ID - Product ID - Amount - Time");
                foreach (var item in stockctrl.GetAll())
                {
                    Console.WriteLine($"{item.Id} - {item.ProductId} - {item.Amount} - {item.Time}");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
          
        }

        /// <summary>
        /// Lists all registered information about stocks from the database.
        /// </summary>
       
        public void Get()
        {

            try
            { 
                Console.Write("Enter stock id: ");
                int id = int.Parse(Console.ReadLine());
                var result = stockctrl.Get(id);
                Console.WriteLine("ID - Product ID - Amount - Time");
                Console.WriteLine($"{result.Id} - {result.ProductId} - {result.Amount} - {result.Time}");
               
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }
        /// <summary>
        /// Gets stock by its start time and end time.
        /// </summary>
        /// <remarks>
        /// Inputs start time and end time.
        /// </remarks>
        /// <remarks>
        /// Lists all information about stocks from the database in real time.
        /// </remarks>
        public void GetByTime()
        {
            try
            {
                Console.WriteLine("Getting stock by time...");
                Console.Write("Enter start time: ");
                DateTime startTime = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter end time: ");
                DateTime endTime = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("ID - Product ID - Amount - Time");
                foreach (var item in stockctrl.GetByTime(startTime, endTime))
                {
                    Console.WriteLine($"{item.Id} - {item.ProductId} - {item.Amount} - {item.Time}");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Adding a stock using the product id or name.
        /// </summary>
        /// <remarks>
        /// Entering product name and amount.
        /// </remarks>
        /// <remarks>
        /// If the result is true, returns a stock with product id, amount and a real time.
        /// </remarks>
        /// <remarks>
        /// Else returns a stock with product name, amount and a real time.
        /// </remarks>
        
        public void Add()
        {
            try
            {
                Console.WriteLine("Adding stock by product id or name...");
                Console.Write("Enter product ID or name: ");
                string product = Console.ReadLine();
                Console.Write("Enter stock amount: ");
                double amount = double.Parse(Console.ReadLine());
                bool result = int.TryParse(product, out int productId);
                if (result)
                {
                    stockctrl.Add(productId, amount, DateTime.Now);
                }
                else
                {
                    stockctrl.Add(product, amount, DateTime.Now);
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Deletes a stock from the database.
        /// </summary>
        
        public void Delete()
        {
            try
            {
                Console.WriteLine("Deleting stock...");
                Console.Write("Enter stock id: ");
                int id = int.Parse(Console.ReadLine());
                stockctrl.Delete(id);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

    }
}
