﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using Models.Models;

namespace Business.Business.Sales
{
    public class DealController : ISalesController<Deal>
    {
        private LuminousContext context = new LuminousContext();
        private User currentUser;
        private ProductController productCtrl;
        public DealController(User currentUser)
        {
            this.currentUser = currentUser;
        }
        public void Add(int productId, double Amount)
        {
            if (Amount > 0)
            {
                var deal = new Deal(currentUser.Id, productId, Amount);
                context.Deal.Add(deal);
                context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Amount cannot be negative");
            }
        }

        public void Add(string productName, double Amount)
        {
            if (Amount > 0)
            {
                productCtrl = new ProductController(currentUser);
                var productId = productCtrl.Get(productName).Id;
                var deal = new Deal(currentUser.Id, productId, Amount);
                context.Deal.Add(deal);
                context.SaveChanges();
            }

            else
            {
                throw new ArgumentException("Amount cannot be negative");
            }
        }

        public void Delete(int id)
        {
            if (currentUser.RoleId > 1)
            {
                var deal = Get(id);
                if (deal != null)
                {
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

        public Deal Get(int id)
        {
            return context.Deal.Find(id);
        }

        public ICollection<Deal> GetAll()
        {
            return context.Deal.ToList();
        }

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
    }
}