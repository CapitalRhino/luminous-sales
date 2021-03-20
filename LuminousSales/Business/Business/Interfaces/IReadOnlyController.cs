using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Business.Interfaces
{
    /// <summary>
    /// Interface used only for read-only Controllers such as RoleController
    /// </summary>
    interface IReadOnlyController<T>
    {
        ICollection<T> GetAll();
        T Get(int id);
        T Get(string name);
        ICollection<T> GetByApproximateName(string name);
    }
}
