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
            if (currentUser.RoleId == 3)
            {
                if (!GetAll().Where(p => p.Name == name).Any())
                {
                    if (price > 0)
                    {
                        var product = new Product(name, price);
                        context.Product.Add(product);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new ArgumentException("Price cannot be negative");
                    }
                }
                else
                {
                    throw new ArgumentException("Item with the given name already exists!");
                }
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void UpdateName(int id, string newName)
        {
            if (currentUser.RoleId == 3)
            {
                var product = Get(id);
                if (product != null)
                {
                    if (!GetAll().Where(p => p.Name == newName).Any())
                    {
                        product.Name = newName;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new ArgumentException("Item with the given name already exists!");
                    }
                }
                else
                {
                    throw new ArgumentException("Product id not valid!");
                }
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void UpdateName(string oldName, string newName)
        {
            if (currentUser.RoleId == 3)
            {
                var product = Get(oldName);
                if (product != null)
                {
                    if (!GetAll().Where(p => p.Name == newName).Any())
                    {
                        product.Name = newName;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new ArgumentException("Item with the given name already exists!");
                    }
                }
                else
                {
                    throw new ArgumentException("Product name not valid!");
                }
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void UpdatePrice(int id, double price)
        {
            if (currentUser.RoleId  == 3)
            {
                var product = Get(id);
                if (product != null)
                {
                    if (price > 0)
                    {
                        product.Price = price;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new ArgumentException("Price cannot be negative");
                    }
                }
                else
                {
                    throw new ArgumentException("Product id not valid!");
                }
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void UpdatePrice(string name, double price)
        {
            if (currentUser.RoleId == 3)
            {
                var product = Get(name);
                if (product != null)
                {
                    if (price > 0)
                    {
                        product.Price = price;
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new ArgumentException("Price cannot be negative");
                    }
                }
                else
                {
                    throw new ArgumentException("Product name not valid!");
                }
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void Delete(int id)
        {
            if (currentUser.RoleId == 3)
            {
                var user = Get(id);
                if (user != null)
                {
                    context.Product.Remove(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("User not found");
                }
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
        public void Delete(string name)
        {
            if (currentUser.RoleId == 3)
            {
                var user = Get(name);
                if (user != null)
                {
                    context.Product.Remove(user);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("User not found");
                }
            }
            else
            {
                throw new ArgumentException("Insufficient Role!");
            }
        }
    }
}