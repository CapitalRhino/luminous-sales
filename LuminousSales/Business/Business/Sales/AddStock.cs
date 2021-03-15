using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;

namespace Business.Business.Sales
{
    public class AddStock
    {
        private LuminousContext productContext;

        public void AddProduct(Product product)
        {
            using (productContext = new LuminousContext())
            {
                productContext.Product.Add(product);
                productContext.SaveChanges();

            }
            
        }
        public void AddStocks(Stock stock)
        {
            using (productContext = new LuminousContext())
            {
                
                productContext.Stock.Add(stock);
                productContext.SaveChanges();
            }
        }

       
    }
}
