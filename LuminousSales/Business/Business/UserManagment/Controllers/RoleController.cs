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
        private LuminousContext context;
        private User currentUser;
        public RoleController(User currentUser)
        {
            this.context = new LuminousContext();
            this.currentUser = currentUser;
        }
        public ICollection<Role> GetAll()
        {
            if (currentUser != null || currentUser.RoleId == 3)
            {
                return context.Role.ToList();
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public Role Get(int id)
        {
            if (currentUser != null || currentUser.RoleId == 3)
            {
                return context.Role.Find(id);
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public Role Get(string name)
        {
            if (currentUser != null || currentUser.RoleId == 3)
            {
                return context.Role.FirstOrDefault(u => u.Name == name);
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public ICollection<Role> GetByApproximateName(string name)
        {
            if (currentUser != null || currentUser.RoleId == 3)
            {
                return context.Role.Where(u => u.Name.Contains(name)).ToList();
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
    }
}
