using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Business.Business.Sales
{
    public class Sales
    {
        private LuminousContext productContext;
        public void Sale(int id)
        {
            using (productContext = new LuminousContext())
            {
                var product = productContext.Product.Find(id);
                if (product != null)
                {
                    productContext.Product.Remove(product);
                    productContext.SaveChanges();
                }
            }
        }



    }
}
