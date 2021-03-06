using Business.Business.UserManagment;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Business.Sales
{
    public class ProductController : IController<Product>
    {
        private LuminousContext context;
        private User currentUser;

        /// <summary>
        /// Constructor that accepts a user object
        /// </summary>
        /// <remarks>
        /// User object is used for role checking
        /// </remarks>

        public ProductController(User currenUser)
        {
            this.currentUser = currenUser;
            this.context = new LuminousContext();
        }

        /// <summary>
        /// Constructor that accepts custom context and a user object
        /// </summary>
        /// <remarks>
        /// Custom context is mainly used for Unit Testing
        /// </remarks>
        /// <remarks>
        /// User object is used for role checking
        /// </remarks>

        public ProductController(User currenUser, LuminousContext context)
        {
            this.currentUser = currenUser;
            this.context = context;
        }

        /// <summary>
        /// Gets All Products
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <returns>
        /// Returns a ICollection of all products.
        /// </returns>

        public ICollection<Product> GetAll()
        {
            return context.Product.ToList();
        }

        /// <summary>
        /// Searches a product by given Id.
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <returns>
        /// Returns an object of the role with the given Id. 
        /// </returns>

        public Product Get(int id)
        {
            var item = context.Product.Find(id);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new ArgumentException("Product Id not found!");
            }
        }

        /// <summary>
        /// Searches a product by given name
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <returns>
        /// Returns an object of the role with the given name.
        /// </returns>

        public Product Get(string name)
        {
            var item = context.Product.FirstOrDefault(p => p.Name == name);
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new ArgumentException("Product name not found!");
            }
        }

        /// <summary>
        /// Searches a product by a given substring
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <returns>
        /// Returns an ICollection of all roles that contain the given substring in their name.
        /// </returns>

        public ICollection<Product> GetByApproximateName(string name)
        {
            var items = context.Product.Where(u => u.Name.Contains(name)).ToList();
            if (items.Any())
            {
                return items;
            }
            else
            {
                throw new ArgumentException("No products added in the database!");
            }
        }

        /// <summary>
        /// Adds an product in the database
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts an item name and price.
        /// </remarks>

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

        /// <summary>
        /// Updates the name of the given product
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts the id for getting the product.
        /// </remarks>

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

        /// <summary>
        /// Updates the name of the given product
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts the current name for getting the product.
        /// </remarks>

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

        /// <summary>
        /// Updates the price of the given product
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts the id for getting the product.
        /// </remarks>

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

        /// <summary>
        /// Updates the price of the given product.
        /// </summary>
        /// <remarks>
        /// Requires Admin role.
        /// </remarks>
        /// <remarks>
        /// Accepts the name for getting the product.
        /// </remarks>

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

        /// <summary>
        /// Adds to the amount of a given product.
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <remarks>
        /// Accepts the product id for getting the product and amount to add
        /// </remarks>

        public void AddAmount(int productId ,double Amount)
        {
            var product = Get(productId);
            if (product != null)
            {
                product.AmountInStock += Amount;
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Product id not valid!");
            }
        }

        /// <summary>
        /// Adds to the amount of a given product.
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <remarks>
        /// Accepts the product name for getting the product and amount to add
        /// </remarks>

        public void AddAmount(string productName, double Amount)
        {
            var product = Get(productName);
            if (product != null)
            {
                product.AmountInStock += Amount;
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Product name not valid!");
            }
        }
        /// <summary>
        /// Subtracts to the amount of a given product.
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <remarks>
        /// Accepts the product id for getting the product and amount to substract
        /// </remarks>

        public void RemoveAmount(int productId, double Amount)
        {
            var product = Get(productId);
            if (product != null)
            {
                product.AmountInStock -= Amount;
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Product id not valid!");
            }
        }

        /// <summary>
        /// Subtracts to the amount of a given product.
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <remarks>
        /// Accepts the product name for getting the product and amount to substract
        /// </remarks>

        public void RemoveAmount(string productName, double Amount)
        {
            var product = Get(productName);
            if (product != null)
            {
                product.AmountInStock -= Amount;
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Product id not valid!");
            }
        }

        /// <summary>
        /// Deletes the given product.
        /// </summary>
        /// <remarks>
        /// Requires Admin Role
        /// </remarks>
        /// <remarks>
        /// Accepts an product for getting the product.
        /// </remarks>


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

        /// <summary>
        /// Deletes the given product
        /// </summary>
        /// <remarks>
        /// Requires Admin role
        /// </remarks>
        /// <remarks>
        /// Accepts an name for getting the product
        /// </remarks>

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