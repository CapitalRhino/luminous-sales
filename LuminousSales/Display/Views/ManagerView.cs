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
        public ManagerView(User currentUser):base(currentUser)
        {
            stockctrl = new StockController(currentUser);
            dealctrl = new DealController(currentUser);
        }

        public void GetAll()
        {
            try
            {
               Console.Write("Getting all stock...");
                
                foreach (var item in stockctrl.GetAll())
                {
                    Console.WriteLine($"{item.Id} {item.ProductId} {item.Amount} ");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
          
        }

        public void Get(int id)
        {

            try
            { 
                Console.WriteLine("Getting stock id...");
                id = int.Parse(Console.ReadLine());
                stockctrl.Get(id);
               
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

        }

        public void GetByTime(DateTime startTime, DateTime endTime)
        {
            try
            {
                Console.WriteLine("Getting stock by time...");
                
                startTime = new DateTime();
                endTime = new DateTime();
                stockctrl.GetByTime(startTime, endTime);

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public void Add(int productId, double amount)
        {
            try
            {
                Console.WriteLine("Adding stock by product id...");
                productId = int.Parse(Console.ReadLine());
                amount = double.Parse(Console.ReadLine());
                stockctrl.Add(productId, amount);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public void Add(string productName, double amount)
        {
            try
            {
                Console.WriteLine("Adding stock by product name...");
                productName = Console.ReadLine();
                amount = double.Parse(Console.ReadLine());
                stockctrl.Add(productName, amount);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                Console.WriteLine("Deleting stock");
                id = int.Parse(Console.ReadLine());
                dealctrl.Delete(id);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

    }
}
