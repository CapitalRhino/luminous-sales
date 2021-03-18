using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business.Sales
{
    interface IStockController<T>
    {
        ICollection<T> GetAll();
        T GetById(int id);
        T GetByName(string name);
        void AddProduct(T product);
        void LoadProductByName(T product);
        void LoadProductById(int id);
        void Sale(int id);
        void Sale(string name);

    }
}
