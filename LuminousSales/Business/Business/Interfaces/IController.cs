using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business.UserManagment
{
    interface IController<T>
    {
        ICollection<T> GetAll();
        T Get(int id);
        T Get(string name);
        ICollection<T> GetByApproximateName(string name);
        void UpdateName(int id, string newName);
        void UpdateName(string oldName, string newName);
        void Delete(int id);
        void Delete(string name);
    }
}
