﻿using System;
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
        public ManagerView(User currentUser):base(currentUser)
        {
            stockctrl = new StockController(currentUser);
            dealctrl = new DealController(currentUser);
        }
        public override void ShowAvaliableCommands()
        {
            base.ShowAvaliableCommands();
            Console.WriteLine("2. Stock");
        }
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
