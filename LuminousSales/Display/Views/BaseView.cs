using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Display.Views
{
    class BaseView
    {
        public virtual void ShowAvaliableCommands()
        {
            Console.WriteLine("1. Sales");
        }
        public void SaleHandle()
        {
            Deal deal = new Deal();
            Product product = new Product();
            while (true)
            {
                SearchItem();
                SaleItem();
            }
        }
        private void SearchItem()
        {

        }
        private void SaleItem()
        {

        }
    }
}
