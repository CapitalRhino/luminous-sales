using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Business.UserManagment;
using Models;
using Models.Models;

namespace Business.Business.Sales
{
    public class DealController : ISalesController<Deal>
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
        
        public DealController(User currentUser)
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

        public DealController(User currentUser, LuminousContext context, ProductController productctrl, UserController userctrl)
        {
            this.currentUser = currentUser;
            this.context = context;
            this.productctrl = productctrl;
            this.userctrl = userctrl;
        }

        /// <summary>
        /// Gets All Deals
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <returns>
        /// Returns a ICollection of all Deals.
        /// </returns>

        public ICollection<Deal> GetAll()
        {
            return context.Deal.ToList();
        }

        /// <summary>
        /// Searches a deal by given Id.
        /// </summary>
        /// <remarks>
        /// Requires Manager role or better.
        /// </remarks>
        /// <returns>
        /// Returns an object of the role with the given Id. 
        /// </returns>
        
        public Deal Get(int id)
        {
            if (currentUser != null || currentUser.RoleId > 1)
            {
                return context.Deal.Find(id);
            }
            else
            {
                throw new ArgumentException("Insufficient Roles");
            }
        }

        /// <summary>
        /// Gets deals between time periods.
        /// </summary>
        /// <remarks>
        /// Requires Manager role or better.
        /// </remarks>
        /// <returns>
        /// Returns a collection of all the deal in the criteria. 
        /// </returns>

        public ICollection<Deal> GetByTime(DateTime startTime, DateTime endTime)
        {
            if (currentUser.RoleId > 1)
            {
                return context.Deal.Where(x => x.Time <= endTime && x.Time >= startTime).ToList();
            }
            else
            {
                throw new ArgumentException("Insufficient role!");
            }
        }

        /// <summary>
        /// Gets deals made by certain user.
        /// </summary>
        /// <remarks>
        /// Accepts user id for getting the user.
        /// Requires Manager role or better.
        /// </remarks>
        /// <returns>
        /// Returns an Collection of all the deals in the criteria. 
        /// </returns>

        public ICollection<Deal> GetByUser(int id)
        {
            if (currentUser != null || currentUser.RoleId > 1)
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

        /// <summary>
        /// Gets deals made by certain user.
        /// </summary>
        /// <remarks>
        /// Accepts username for getting the user.
        /// Requires Manager role or better.
        /// </remarks>
        /// <returns>
        /// Returns an Collection of all the deals in the criteria. 
        /// </returns>

        public ICollection<Deal> GetByUser(string username)
        {
            if (currentUser != null || currentUser.RoleId > 1)
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

        /// <summary>
        /// Adds Deal to the database.
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <remarks>
        /// Accepts product id for getting the product, amount sold and time of transaction.
        /// </remarks>

        public void Add(int productId, double Amount, DateTime time)
        {
            if (Amount > 0)
            {
                var deal = new Deal(currentUser.Id, productId, Amount, time);
                productctrl.RemoveAmount(productId, Amount);
                context.Deal.Add(deal);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Amount cannot be negative");
            }
        }

        /// <summary>
        /// Adds Deal to the database.
        /// </summary>
        /// <remarks>
        /// Requires no special roles.
        /// </remarks>
        /// <remarks>
        /// Accepts product name for getting the product, amount sold and time of transaction.
        /// </remarks>

        public void Add(string productName, double Amount, DateTime time)
        {
            if (Amount > 0)
            {
                productctrl = new ProductController(currentUser);
                var productId = productctrl.Get(productName).Id;
                var deal = new Deal(currentUser.Id, productId, Amount, time);
                productctrl.RemoveAmount(productId, Amount);
                context.Deal.Add(deal);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Amount cannot be negative");
            }
        }

        /// <summary>
        /// Deletes Deal from the database.
        /// </summary>
        /// <remarks>
        /// Requires Manager Role or better.
        /// </remarks>
        /// <remarks>
        /// Accepts product id for getting the product
        /// </remarks>

        public void Delete(int id)
        {
            if (currentUser != null || currentUser.RoleId > 1)
            {
                var deal = Get(id);
                if (deal != null)
                {
                    productctrl.AddAmount(deal.ProductId, deal.Amount);
                    context.Deal.Remove(deal);
                    context.SaveChanges();
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
    }
}
