using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business.Sales
{
    interface ISalesController<T>
    {
        ICollection<T> GetAll();
        T Get(int id);
        ICollection<T> GetByTime(DateTime startTime, DateTime endTime);
        void Add(int productId, double Amount, DateTime time);
        void Add(string productName, double Amount, DateTime time);
        void Delete(int id);
    }
}
