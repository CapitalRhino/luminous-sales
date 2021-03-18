using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Business.Business.UserManagment
{
    public class UserController : IController<User>
    {
        private LuminousContext context = new LuminousContext();

        public ICollection<User> GetAll()
        {
            return context.User.ToList();
        }
        public void DeleteById()
        {
            throw new NotImplementedException();
        }

        public void DeleteByName()
        {
            throw new NotImplementedException();
        }
        public User SearchById(int id)
        {
            return context.User.FirstOrDefault(u => u.Id == id);
        }

        public ICollection<User> SearchByApproximateName(string name)
        {
            return context.User.Where(u => u.Name.Contains(name)).ToList();
        }
        public User SearchByExactName(string name)
        {
            return context.User.FirstOrDefault(u => u.Name == name);
        }

        public void UpdateNameById(int id, string newName)
        {
            var user = SearchById(id);
            if (user != null)
            {
                if (user.Name != newName)
                {
                    user.Name = newName;
                }
                else
                {
                    throw new ArgumentException("Usernames match. Please choose another username!");
                }
            }
            else
            {
                throw new ArgumentException("No user with such id");
            }
        }

        public void UpdateNameByOldName(string oldName, string newName)
        {
            if (oldName != newName)
            {
                var user = SearchByExactName(oldName);
                if (user != null)
                {
                    user.Name = newName;
                }
                else
                {
                    throw new ArgumentException("No user with such name!");
                }
            }
            else
            {
                throw new ArgumentException("Usernames match. Please use another username!");
            }

        }
        public void UpdatePasswordById(int id, string newPassword)
        {
            var user = SearchById(id);
            if (user != null)
            {
                if (user.Password != newPassword)
                {
                    user.Password = newPassword;
                }
                else
                {
                    throw new ArgumentException("Passwords match! Please use another password!");
                }
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }
        public void UpdatePasswordByName(string name, string newPassword)
        {
            var user = SearchByExactName(name);
            if (user != null)
            {
                if (user.Password != newPassword)
                {
                    user.Password = newPassword;
                }
                else
                {
                    throw new ArgumentException("Passwords match! Please use another password!");
                }
            }
            else
            {
                throw new ArgumentException("User not found");
            }
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
