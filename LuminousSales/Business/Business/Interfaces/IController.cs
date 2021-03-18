using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business.UserManagment
{
    interface IController<T>
    {
        ICollection<T> GetAll();
        T SearchById(int id);
        ICollection<T> SearchByApproximateName(string name);
        T SearchByExactName(string name);
        void UpdateNameById(int id, string newName);
        void UpdateNameByOldName(string oldName, string newName);
        void DeleteById(int id);
        void DeleteByName(string name);
    }
}
