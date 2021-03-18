using Business.Business.Interfaces;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Business.UserManagment.Controllers
{
    class RoleController : IReadOnlyController<Role>
    {
        private LuminousContext context = new LuminousContext();

        public ICollection<Role> GetAll()
        {
            return context.Role.ToList();
        }
        public Role Get(int id)
        {
            return context.Role.Find(id);
        }
        public Role Get(string name)
        {
            return context.Role.FirstOrDefault(u => u.Name == name);
        }
        public ICollection<Role> GetByApproximateName(string name)
        {
            return context.Role.Where(u => u.Name.Contains(name)).ToList();
        }
    }
}
