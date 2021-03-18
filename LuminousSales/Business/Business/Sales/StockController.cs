using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;

namespace Business.Business.Sales
{
    public class StockController : ISalesController<Product>
    {
        private LuminousContext context;

        Product ISalesController<Product>.Get(int id)
        {
            return context.Product.Find(id);
        }
        ICollection<Product> ISalesController<Product>.GetByTime(DateTime time)
        {
            throw new NotImplementedException();
        }
        void ISalesController<Product>.Add(int productId, double Amount)
        {
            throw new NotImplementedException();
        }
        void ISalesController<Product>.Add(string productName, double Amount)
        {
            throw new NotImplementedException();
        }
        void ISalesController<Product>.Delete(int id)
        {
            throw new NotImplementedException();
        }
        public ICollection<Product> GetAll()
        {
            using (context = new LuminousContext())
            {
                return context.Product.ToList();
            }
            
        }
        public void AddProduct(Product product)
        {
            using (context = new LuminousContext())
            {
             
                context.Product.Add(product);
                context.SaveChanges();

            }
            
        }
        public void LoadProduct(int id)
        {
            using (context = new LuminousContext())
            {
                var item = context.Product.Find(id);
                if (item != null)
                {
                    context.Entry(item).CurrentValues.SetValues(id);
                    context.SaveChanges();
                }
            }
        }

        public void LoadProduct(Product product)
        {
            using (context = new LuminousContext())
            {
                var item = context.Product.Find(product.Id);
                if (item !=null)
                {
                    context.Entry(item).CurrentValues.SetValues(product);
                    context.SaveChanges();
                }
            }
        }

        public void Sale(int id)
        {
            using (context = new LuminousContext())
            {
                var product = context.Product.Find(id);
                if (product != null)
                {
                    context.Product.Remove(product);
                    context.SaveChanges();
                }
            }
        }

        public void Sale(string name)
        {
            using (context = new LuminousContext())
            {
                var product = context.Product.Find(name);
                if (product !=null)
                {
                    context.Product.Remove(product);
                    context.SaveChanges();
                }
            }
        }
    }

}

