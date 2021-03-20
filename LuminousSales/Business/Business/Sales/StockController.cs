using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Business.UserManagment;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;

namespace Business.Business.Sales
{
    public class StockController : ISalesController<Stock>
    {
        private LuminousContext context;
        private User currentUser;
        private ProductController productctrl;
        private UserController userctrl;

        /// <summary>
        /// Constructor that accepts a user object
        /// </summary>
        /// <remarks>
        /// User object is used for role checking
        /// </remarks>

        public StockController(User currentUser)
        {
            this.currentUser = currentUser;
            this.context = new LuminousContext();
            this.productctrl = new ProductController(currentUser);
            this.userctrl = new UserController(currentUser);
        }

        /// <summary>
        /// Constructor that accepts custom context, ProductController, UserController  and a user object
        /// </summary>
        /// <remarks>
        /// Mainly Used for Unit Teststing
        /// </remarks>
        /// <remarks>
        /// User object is used for role checking
        /// </remarks>

        public StockController(User currentUser, LuminousContext context ,ProductController productctrl, UserController userctrl)
        {
            this.currentUser = currentUser;
            this.context = context;
            this.productctrl = productctrl;
            this.userctrl = userctrl;
        }

        /// <summary>
        /// Gets All Stocks
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <returns>
        /// Returns a ICollection of all Deals.
        /// </returns>

        public ICollection<Stock> GetAll()
        {
            if (currentUser != null || currentUser.RoleId > 1)
            {
                return context.Stock.ToList();
            }
            else
            {
                throw new ArgumentException("Cannot return all stocks!");
            }
             
        }

        /// <summary>
        /// Searches a stocks session by given Id.
        /// </summary>
        /// <remarks>
        /// Requires Manager role or better.
        /// </remarks>
        /// <returns>
        /// Returns an object of the role with the given Id. 
        /// </returns>

        public Stock Get(int id)
        {
            if (currentUser != null || currentUser.RoleId > 1)
            {
                return context.Stock.Find(id);
            }
            else
            {
                throw new ArgumentException("Insufficient Roles");
            }
        }

        /// <summary>
        /// Gets stocks between time periods.
        /// </summary>
        /// <remarks>
        /// Requires Manager role or better.
        /// </remarks>
        /// <returns>
        /// Returns a collection of all the stocks in the criteria. 
        /// </returns>

        public ICollection<Stock> GetByTime(DateTime startTime, DateTime endTime)
        {
            if (currentUser != null || currentUser.RoleId > 1)
            {
                return context.Stock.Where(x => x.Time <= endTime && x.Time >= startTime).ToList();
            }
            else
            {
                throw new ArgumentException("Insufficient role!");
            }
        }

        public ICollection<Stock> GetByUser(int id)
        {
            if (currentUser != null || currentUser.RoleId == 3)
            {
                var user = userctrl.Get(id);
                if (user != null)
                {
                    return GetAll().Where(u => u.UserId == user.Id).ToList();
                }
                else
                {
                    throw new ArgumentException("User not found");
                }
            }
            else
            {
                throw new ArgumentException("Insufficient role!");
            }
        }

        public ICollection<Stock> GetByUser(string username)
        {
            if (currentUser != null || currentUser.RoleId == 3)
            {
                var user = userctrl.Get(username);
                if (user != null)
                {
                    return GetAll().Where(u => u.UserId == user.Id).ToList();
                }
                else
                {
                    throw new ArgumentException("User not found");
                }
            }
            else
            {
                throw new ArgumentException("Insufficient role!");
            }
        }

        public void Add(int productId, double Amount, DateTime time)
        {
            if (currentUser.RoleId > 1)
            {
                if (Amount > 0)
                {
                    var stock = new Stock(currentUser.Id, productId, Amount, time);
                    productctrl.AddAmount(productId, Amount);
                    context.Stock.Add(stock);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Amount cannot be negative");
                }

            }
            else
            {
                throw new ArgumentException("Insufficient role!");
            }
        }

        public void Add(string productName, double Amount, DateTime time)
        {
            if (currentUser.RoleId > 1)
            {
                if (Amount > 0)
                {
                    productctrl = new ProductController(currentUser);
                    var productId = productctrl.Get(productName).Id;
                    var stock = new Stock(currentUser.Id, productId, Amount, time);
                    productctrl.AddAmount(productId, Amount);
                    context.Stock.Add(stock);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Amount cannot be negative");
                }

            }
            else
            {
                throw new ArgumentException("Insufficient role!");
            }
             
        }

        public void Delete(int id)
        {
            if (currentUser.RoleId == 3 )
            {
                var stock = Get(id);
                if (stock != null)
                {
                    productctrl.RemoveAmount(stock.ProductId, stock.Amount);
                    context.Stock.Remove(stock);
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("Stock Id not found!");
                }

            }
            else
            {
                throw new ArgumentException("Insufficient role!");
            }
        }
    }

}

