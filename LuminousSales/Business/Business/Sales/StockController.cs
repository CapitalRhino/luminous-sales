﻿using System;
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
        private LuminousContext context = new LuminousContext();
        private User currentUser;
        private ProductController productCtrl;
        private UserController userctrl;

        public StockController(User currentUser)
        {
            this.currentUser = currentUser;
            this.productCtrl = new ProductController(currentUser);
            this.userctrl = new UserController(currentUser);
        }

        public StockController(User currentUser, ProductController productctrl, UserController userctrl)
        {
            this.currentUser = currentUser;
            this.productCtrl = new ProductController(currentUser);
            this.userctrl = new UserController(currentUser);
        }
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

        public Stock Get(int id)
        {
            if (currentUser != null || currentUser.RoleId > 1)
            {
                return context.Stock.Find(id);
            }
            else
            {
                throw new ArgumentException("Cannot get stock!");
            }
        }

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
                    productCtrl.AddAmount(productId, Amount);
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
                    productCtrl = new ProductController(currentUser);
                    var productId = productCtrl.Get(productName).Id;
                    var stock = new Stock(currentUser.Id, productId, Amount, time);
                    productCtrl.AddAmount(productId, Amount);
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
                    productCtrl.RemoveAmount(stock.ProductId, stock.Amount);
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

