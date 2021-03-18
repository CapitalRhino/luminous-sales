using Business.Business.UserManagment;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Business.Sales
{
    class ProductController : IController<Product>
    {
        private LuminousContext context = new LuminousContext();
        private User currentUser;
        public ProductController(User currenUser)
        {
            this.currentUser = currenUser;
        }
        public ICollection<Product> GetAll()
        {
            return context.Product.ToList();
        }
        public Product Get(int id)
        {
            return context.Product.Find(id);
        }
        public Product Get(string name)
        {
            return context.Product.FirstOrDefault(p => p.Name == name);
        }
        public ICollection<Product> GetByApproximateName(string name)
        {
            return context.Product.Where(u => u.Name.Contains(name)).ToList();
        }
        public void AddItem(string name, double price)
        {
            var product = new Product(name, price);
            context.Product.Add(product);
        }
        public void UpdateName(int id, string newName)
        {
            if (currentUser.RoleId > 1)
            {
                var product = Get(id);
                if (product != null)
                {

                }
                else
                {

                }
            }
        }
        public void UpdateName(string oldName, string newName)
        {
            throw new NotImplementedException();
        }
        public void UpdatePrice(int id)
        {

        }
        public void UpdatePrice(string name)
        {

        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string name)
        {
            throw new NotImplementedException();
        }

    }
}
