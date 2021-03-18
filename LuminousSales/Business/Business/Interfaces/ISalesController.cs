﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business.Sales
{
    interface ISalesController<T>
    {
        ICollection<T> GetAll();
        T Get(int id);
        ICollection<T> GetByTime(byte[] startPeriod, byte[] endPeriod);
        void Add(int productId, double Amount);
        void Add(string productName, double Amount);
        void Delete(int id);
    }
}
